using OneCode.EnumTypes;
using System;
using System.Text.Json.Serialization;

namespace OneCode.Orders.Dtos
{
    public class OrderDto
    {
        public string Title { get; set; }

        public decimal TotalAmount { get; set; }

        //public decimal TotalPorfit { get; set; }

        public decimal TotalCommision { get; set; }

        public decimal SettlementAmount { get; set; }

        public Guid ShopId { get; set; }

        public string ShopName { get; set; }

        public Guid? SalerId { get; set; }

        public string SalerName { get; set; }

        public OrderStatus OrderStatus { get; set; }

        [JsonConverter(typeof(DateFormat))]
        public string CreationTime { get; set; }

        public string FinishedDate { get; set; }

        public OrderBizStatus BizStatus { get; set; }

        public string OutterOrderId { get; set; }

    }
}
