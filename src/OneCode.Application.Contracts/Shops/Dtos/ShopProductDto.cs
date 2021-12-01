using OneCode.EnumTypes;
using OneCode.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Shops.Dtos
{
    public class ShopProductDto
    {
        public ProductDto Product { get; set; }

        public CommisionTypeEnum CommisionType { get; set; }

        public decimal CommisionRate { get; set; }

        public decimal CommisionValue { get; set; }

    }
}
