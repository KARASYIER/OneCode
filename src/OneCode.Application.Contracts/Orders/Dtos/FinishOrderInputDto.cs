using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Orders.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class FinishOrderInputDto
    {
        /// <summary>
        /// 外部订单号
        /// </summary>
        public string OutterOrderId { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 订单总利润
        /// </summary>
        public decimal TotalProfit { get; set; }
    }
}
