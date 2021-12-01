using OneCode.Application.Contracts;
using OneCode.Domain;
using OneCode.Domain.Repositories;
using OneCode.Dtos;
using OneCode.EnumTypes;
using OneCode.Orders.Dtos;
using OneCode.ToolKit.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OneCode.Application
{
    public class OrderAppService : OneCodeAppService, IOrderAppService
    {
        private IOrderRepository _orderRepository;
        private ISalerRepository _salerRepository;
        private IShopRepository _shopRepository;
        private IProductRepository _productRepository;
        private IShopProductRepository _shopProductRepository;
        private ICommisionRecordRepository _commisionRecordRepository;

        public OrderAppService(
            IOrderRepository orderRepository,
            ISalerRepository salerRepository,
            IShopRepository shopRepository,
            IProductRepository productRepository,
            IShopProductRepository shopProductRepository,
            ICommisionRecordRepository commisionRecordRepository
            )
        {
            _orderRepository = orderRepository;
            _salerRepository = salerRepository;
            _shopRepository = shopRepository;
            _productRepository = productRepository;
            _shopProductRepository = shopProductRepository;
            _commisionRecordRepository = commisionRecordRepository;
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<ResponseReturn> CreateAsync(CreateOrderInputDto input)
        {
            //检查input的空值
            input.CheckInput();

            //创建一个订单
            var order = new Order(GuidGenerator.Create())
            {
                //设置默认属性
            };

            //将input参数写入order
            ObjectMapper.Map(input, order);

            //检查重复订单号
            (await _orderRepository.AnyAsync(p => p.OutterOrderId == order.OutterOrderId)).CheckBool(OneCodeDomainErrorCodes.ErrMsg_3002);

            //获取店铺
            var shop = (await _shopRepository.FindAsync(order.ShopId)).CheckNullOrDeleted(OneCodeDomainErrorCodes.ErrMsg_3008);

            order.ShopName = shop.Name;


            if (input.SalerId.HasValue)
            {
                //检查分销员Id
                var saler = (await _salerRepository.FindAsync(order.SalerId.Value)).CheckNullOrDeleted(OneCodeDomainErrorCodes.ErrMsg_3009);

                order.SalerName = saler.Name;
            }

            foreach (var detail in input.Details)
            {
                //检查产品是否存在
                var product = (await _productRepository.GetAsync(detail.OutterProductId, detail.ProductType)).CheckNullOrDeleted(OneCodeDomainErrorCodes.ErrMsg_3003);

                //此处需要优化
                var shopProduct = (await _shopProductRepository.FindAsync(p => p.ShopId.Equals(shop.Id) && p.ProductId.Equals(product.Id))).CheckNull(OneCodeDomainErrorCodes.ErrMsg_3003);

                var orderdetail = new OrderDetail(GuidGenerator.Create(), order.Id)
                {
                    ProductId = product.Id,
                    ProductTypeName = product.TypeName,

                    //产品佣金相关属性
                    CommisionType = shopProduct.CommisionType,
                    CommisionRate = shopProduct.CommisionRate,
                    CommisionValue = shopProduct.CommisionValue,

                    RefundBalance = 0, //退款平衡差额 = 0

                    RemainAmount = input.TotalAmount, //订单创建,剩余金额=总金额
                    RemainSettlementAmount = input.TotalAmount //订单创建,结算金额=总金额
                };

                //提交的值映射到对象
                ObjectMapper.Map(detail, orderdetail);

                order.Title += (order.Title.IsNullOrEmpty() ? "" : "，") + detail.ProductTitle;

                order.OrderDetails.Add(orderdetail);

            }
            //校验金额
            CheckAmount(order, input.TotalAmount);

            //结算金额(初始化结算金额=总金额)
            order.SettlementAmount = input.TotalAmount;

            //计算明细佣金
            CalculateOrderDetailCommision(order);

            order.TotalCommision = order.OrderDetails.Sum(p => p.Commision);
            //订单初始状态
            order.BizStatus = OrderBizStatus.Normal;
            order.OrderStatus = OrderStatus.Doing;

            //店铺,进行中的佣金 增加
            shop.CommisionDoing += order.TotalCommision;


            //优化:将多个不同表的新增数据,放入一个context利用事务提交
            await _orderRepository.CreateOrderAsync(order, shop);

            return ResponseReturn.ReturnSuccess(
                data: ObjectMapper.Map<Order, CreateOrderResultDto>(order)
                );
        }

        /// <summary>
        /// 部分退款
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> UpdateRefundPartialAsync(RefundPartialOrderInputDto input)
        {
            //input空值判断
            input.CheckInput();

            //根据外部订单号获取订单
            var order = (await _orderRepository.GetByOutterIdAsync(input.OutterOrderId)).CheckNullOrDeleted(OneCodeDomainErrorCodes.ErrMsg_3001);

            (order.OrderDetails == null || order.OrderDetails.Count <= 0).CheckBool(3006);

            (order.OrderStatus == OrderStatus.Finished).CheckBool(3007);

            var refundOrderDetails = new List<OrderDetail>();

            foreach (var refund in input.RefundDetails)
            {
                //获取产品
                var product = (await _productRepository.FindAsync(p => p.OutterId == refund.OutterProductId && p.TypeId == refund.ProductType)).CheckNullOrDeleted(OneCodeDomainErrorCodes.ErrMsg_3004);

                //获取下单相关的明细
                var originOrderDetail = (order.OrderDetails.First(p => p.OutterProductId == refund.OutterProductId && p.ProductType == refund.ProductType)).CheckNullOrDeleted(OneCodeDomainErrorCodes.ErrMsg_3006);

                //新增一条负值明细
                var createOrderDetail = new OrderDetail(GuidGenerator.Create(), order.Id)
                {
                    ProductId = product.Id,
                    ProductTypeName = product.TypeName,

                    CommisionType = originOrderDetail.CommisionType,
                    CommisionRate = originOrderDetail.CommisionRate,
                    CommisionValue = originOrderDetail.CommisionValue,
                };

                //填充数据
                ObjectMapper.Map(refund, createOrderDetail);

                //明细退的佣金 = ((明细金额 + 差额) * 百分比)(负值)

                //退款金额(负值)
                createOrderDetail.Amount = refund.RefundAmount.ToNegative();

                //退款(平衡)差额(负值)
                createOrderDetail.RefundBalance = refund.RefundBalance.ToNegative();

                //退款数量(负值)
                createOrderDetail.Count = refund.RefundCount.ToNegative();

                //计算明细佣金(计算结果是负数)
                CalculateOrderDetailCommision(createOrderDetail);

                //佣金设为负数
                //createOrderDetail.Commision = createOrderDetail.Commision.ToNegative();

                //总金额-退款金额
                order.TotalAmount = order.TotalAmount + createOrderDetail.Amount;

                //记录剩余总金额
                createOrderDetail.RemainAmount = order.TotalAmount;

                //结算金额(目前不计算差额的佣金)
                //结算金额 = 结算金额 - 退款金额 - 退款差额
                order.SettlementAmount += (createOrderDetail.Amount + createOrderDetail.RefundBalance);

                //计算差额的佣金(不扣除差额)
                //结算金额 = 结算金额 - 退款金额
                //order.SettlementAmount += (createOrderDetail.Amount);


                //记录剩余结算金额
                createOrderDetail.RemainSettlementAmount = order.SettlementAmount;

                //minusCommision += createOrderDetail.Commision;

                refundOrderDetails.Add(createOrderDetail);
                //order.OrderDetails.Add(createOrderDetail);
                //await _orderDetailsRepository.InsertAsync(orderDetail);

            }

            //验证金额
            //(原始订单明细金额总和 - 退款明细金额综合) = 提交的订单剩余总额
            (order.OrderDetails.Sum(p => p.Amount) - Math.Abs(refundOrderDetails.Sum(p => p.Amount)) != input.BalanceAmount).CheckBool(3005);

            //修改剩余总金额
            order.TotalAmount = input.BalanceAmount;

            //计算佣金
            //CalculateCommision(order);
            order.TotalCommision -= Math.Abs(refundOrderDetails.Sum(p => p.Commision));

            if (input.IsRefundFull)
            {
                order.BizStatus = OrderBizStatus.FullRefund;
                order.OrderStatus = OrderStatus.Finished;

                order.FinishedDate = DateTime.Now;

                //整单退,结算金额 = 0
                //展示时,使用此数值
                order.SettlementAmount = 0;

                //整单退,佣金 = 0
                order.TotalCommision = 0;
            }
            else
            {
                order.BizStatus = OrderBizStatus.PartialRefund;
            }

            var shop = await _shopRepository.GetAsync(order.ShopId);

            shop.CommisionDoing -= refundOrderDetails.Sum(p => p.Commision).ToAbs();

            await _orderRepository.RefundPartailAsync(order, refundOrderDetails, shop);

            return ResponseReturn.ReturnSuccess(
                data: ObjectMapper.Map<Order, OrderDto>(order)
                );
        }

        /// <summary>
        /// 订单完成
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> UpdateFinishAsync(FinishOrderInputDto input)
        {
            //检查input的空值
            input.CheckInput();

            //校验外部订单号
            var order = (await _orderRepository.GetByOutterIdAsync(input.OutterOrderId)).CheckNullOrDeleted(OneCodeDomainErrorCodes.ErrMsg_3001);

            //校验金额
            (order.TotalAmount != input.TotalAmount).CheckBool(3005);

            //校验订单状态
            (order.OrderStatus == OrderStatus.Finished).CheckBool(3007);

            order.OrderStatus = OrderStatus.Finished;
            order.FinishedDate = DateTime.Now;

            //获取店铺信息
            var shop = (await _shopRepository.FindAsync(order.ShopId)).CheckNullOrDeleted(OneCodeDomainErrorCodes.ErrMsg_3008);

            shop.CommisionAvailable += order.TotalCommision;
            shop.CommisionDoing -= order.TotalCommision;

            //记录一笔record
            //var saler = (await _salerRepository.FindAsync(order.SalerId)).CheckNullOrDeleted();

            var record = new CommisionRecord
            {
                RecordFlag = EnumTypes.RecordFlag.Entry,
                RelationId = order.Id,
                ShopId = order.ShopId,
                ShopName = order.ShopName,
                SalerId = order.SalerId,
                SalerName = order.SalerName,
                CommisionAmount = order.TotalCommision,
                CommisionAvailable = shop.CommisionAvailable
            };

            //更新数据
            await _orderRepository.FinishAsync(order, shop, record);

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询单笔
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> GetAsync(Guid id)
        {
            var order = (await _orderRepository.FindAsync(id, true)).CheckNullOrDeleted();
            return ResponseReturn.ReturnSuccess(
                 data: ObjectMapper.Map<Order, OrderFullDto>(order)
                );
        }

        /// <summary>
        /// 根据外部订单号获取单笔订单
        /// </summary>
        /// <param name="outterOrderId"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> GetByOutterOrderIdAsync(string outterOrderId)
        {
            var order = (await _orderRepository.FindAsync(p => p.OutterOrderId == outterOrderId, true)).CheckNullOrDeleted();

            return ResponseReturn.ReturnSuccess(
             data: ObjectMapper.Map<Order, OrderFullDto>(order)
            );
        }

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> GetListAsync(GetOrdersInputDto input)
        {
            var total = await _orderRepository.GetCountAsync(
                                                input.Filter,
                                                input.OutterOrderId,
                                                input.ShopId,
                                                input.SalerId,
                                                input.StartDate,
                                                input.EndDate,
                                                input.OrderStatus,
                                                input.BizStatus);

            var orders = total == 0 ? new List<Order>() :
                                 await _orderRepository.GetListAsync(
                                                  input.Filter,
                                                  input.OutterOrderId,
                                                  input.ShopId,
                                                  input.SalerId,
                                                  input.StartDate,
                                                  input.EndDate,
                                                  input.OrderStatus,
                                                  input.BizStatus,
                                                  input.PageNo,
                                                  input.PageSize);

            return ResponseReturn.ReturnSuccess(
              data: new PagedListResultDto<OrderFullDto>
              {
                  Items = ObjectMapper.Map<List<Order>, List<OrderFullDto>>(orders),
                  TotalCount = total,
                  PageNo = input.PageNo,
                  PageSize = input.PageSize
              });
        }

        /// <summary>
        /// 验证金额利润有效性
        /// </summary>
        /// <param name="order"></param>
        protected void CheckAmount(Order order, decimal totalAmount)
        {
            if (totalAmount != order.OrderDetails.Sum(p => p.Amount))
            {
                OneCodeCheck.OneCodeBizException(3005);
            }
        }

        /// <summary>
        /// 计算佣金
        /// </summary>
        /// <param name="order"></param>
        private void CalculateOrderDetailCommision(Order order)
        {
            foreach (var detail in order.OrderDetails)
            {
                CalculateOrderDetailCommision(detail);
            }
        }

        /// <summary>
        /// 计算佣金
        /// </summary>
        /// <param name="orderDetail"></param>
        private void CalculateOrderDetailCommision(OrderDetail orderDetail)
        {
            switch (orderDetail.CommisionType)
            {
                case CommisionTypeEnum.Fixed:
                    orderDetail.Commision = orderDetail.CommisionValue * orderDetail.Count;
                    break;
                case CommisionTypeEnum.Rate:
                    //不结算差额的佣金
                    //当前业务逻辑,如有差额,不计算佣金,所以一起退
                    //(也就是退款时,把差额的佣金一起退了,所以要加上退款的佣金,退款时佣金是负值)
                    orderDetail.Commision = orderDetail.CommisionRate * (orderDetail.Amount + orderDetail.RefundBalance);

                    //结算差额的佣金(退款时不扣去差额的佣金)
                    //保留未来可能变动的需求
                    //orderDetail.Commision = orderDetail.CommisionRate * (orderDetail.Amount);
                    break;
                default:
                    OneCodeCheck.OneCodeBizException(9002); break;
            }
        }
    }
}
