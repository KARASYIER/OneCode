using System;
using System.Collections.Generic;

namespace OneCode.Orders.Dtos
{
    public class CreateOrderInputDto
    {
        /// <summary>
        /// 外部订单号
        /// </summary>
        public string OutterOrderId { get; set; }

        /// <summary>
        /// 分销店铺Id
        /// </summary>
        public Guid ShopId { get; set; }

        /// <summary>
        /// 分销员Id
        /// </summary>
        public Guid? SalerId { get; set; }

        /// <summary>
        /// 订单总价
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 订单总利润
        /// </summary>
        public decimal TotalPorfit { get; set; }

        /// <summary>
        /// 明细清单
        /// </summary>
        public List<CreateOrderDetailInputDto> Details { get; set; }
    }
}
