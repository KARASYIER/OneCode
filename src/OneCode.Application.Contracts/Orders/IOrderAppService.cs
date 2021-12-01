using OneCode.Dtos;
using OneCode.Orders.Dtos;
using OneCode.ToolKit.Http;
using System;
using System.Threading.Tasks;

namespace OneCode.Application.Contracts
{
    /// <summary>
    /// 处理订单通知的操作接口
    /// </summary>
    public interface IOrderAppService
    {

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> CreateAsync(CreateOrderInputDto input);

        /// <summary>
        /// 部分退款
        /// </summary>
        /// <returns></returns>
        Task<ResponseReturn> UpdateRefundPartialAsync(RefundPartialOrderInputDto input);

        /// <summary>
        /// 订单完成
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateFinishAsync(FinishOrderInputDto input);

        /// <summary>
        /// 查询订单(包含明细)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetAsync(Guid id);

        /// <summary>
        /// 根据外部订单号查询订单(包含明细)
        /// </summary>
        /// <param name="outterOrderId"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetByOutterOrderIdAsync(string outterOrderId);

        /// <summary>
        /// 分页查询订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetListAsync(GetOrdersInputDto input);
    }
}
