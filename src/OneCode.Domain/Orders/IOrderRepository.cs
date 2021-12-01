using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OneCode.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="order"></param>
        /// <param name="shop"></param>
        /// <returns></returns>
        Task CreateOrderAsync(Order order, Shop shop);

        /// <summary>
        /// 部分退款
        /// </summary>
        /// <param name="updateOrder">更新订单</param>
        /// <param name="addRefundOrderDetails">新增退款的明细</param>
        /// <param name="updateShopCommision">更新店铺佣金属性</param>
        /// <returns></returns>
        Task RefundPartailAsync(Order updateOrder, List<OrderDetail> addRefundOrderDetails, Shop updateShopCommision);

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="order"></param>
        /// <param name="addCommisionRecord"></param>
        /// <returns></returns>
        Task FinishAsync(Order order, Shop shop, CommisionRecord addCommisionRecord);

        /// <summary>
        /// 根据外部订单号查询订单
        /// </summary>
        /// <param name="outterId"></param>
        /// <returns></returns>
        Task<Order> GetByOutterIdAsync(string outterId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderid"></param>
        /// <param name="id"></param>
        /// <param name="shopId"></param>
        /// <param name="salerId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="orderStatus"></param>
        /// <param name="orderBizStatus"></param>
        /// <returns></returns>
        Task<long> GetCountAsync(string filter, string outterOrderId, Guid? shopId, Guid? salerId, DateTime? startDate, DateTime? endDate, OrderStatus? orderStatus, OrderBizStatus? orderBizStatus);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderid"></param>
        /// <param name="id"></param>
        /// <param name="shopId"></param>
        /// <param name="salerId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="orderStatus"></param>
        /// <param name="orderBizStatus"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<Order>> GetListAsync(string filter, string outterOrderId, Guid? shopId, Guid? salerId, DateTime? startDate, DateTime? endDate, OrderStatus? orderStatus, OrderBizStatus? orderBizStatus, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 获取累计佣金
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<decimal> GetAccumulatedCommisionAsync(Guid shopId, Guid? salerId, bool isOwner = false);

        /// <summary>
        /// 获取当日佣金
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<decimal> GetTodayCommisionAsync(Guid shopId, Guid? salerId);
    }
}
