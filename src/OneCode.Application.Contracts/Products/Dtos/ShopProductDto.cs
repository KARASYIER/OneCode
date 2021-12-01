using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Products.Dtos
{
    public class ShopProductDto
    {
        public int DisplayOrder { get; set; }

        public decimal CommisionRate { get; set; }

        public ProductDto Product { get; set; }
    }
}
