using OneCode.EnumTypes;
using System;

namespace OneCode.Orders.Dtos
{
    public class OrderDetailDto
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public string ProductTypeName { get; set; }

        public ProductTypeEnum ProductType { get; set; }

        public string ProductTitle { get; set; }

        public string OutterProductId { get; set; }

        public decimal Amount { get; set; }

        public int Count { get; set; }

        public CommisionTypeEnum CommisionType { get; set; }

        public decimal CommisionRate { get; set; }

        public decimal CommisionValue { get; set; }

        public decimal Commision { get; set; }


    }
}
