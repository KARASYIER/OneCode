using System.Collections.Generic;

namespace OneCode.Orders.Dtos
{
    /// <summary>
    /// 部分退款
    /// </summary>
    public class RefundPartialOrderInputDto
    {
        /// <summary>
        /// 外部订单号
        /// </summary>
        public string OutterOrderId { get; set; }

        /// <summary>
        /// 剩余总金额
        /// </summary>
        public decimal BalanceAmount { get; set; }

        /// <summary>
        /// 是否整单退款
        /// </summary>
        public bool IsRefundFull { get; set; }
        /// <summary>
        /// 退单明细
        /// </summary>
        public List<RefundPartialOrderDetailInputDto> RefundDetails { get; set; }
    }
}
