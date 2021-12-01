using Newtonsoft.Json;
using OneCode.EnumTypes;
using System;

namespace OneCode.Products.Dtos
{
    public class ProductRelatedShopDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal OriginCommisionRate { get; set; }

        public decimal OriginCommisionValue { get; set; }

        public CommisionTypeEnum CommisionType { get; set; }

        public decimal CommisionRate { get; set; }

        public decimal CommisionValue { get; set; }
    }
}
