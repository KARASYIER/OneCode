using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Shops.Dtos
{
    public class ShopUnrelatedProductDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public CommisionTypeEnum CommisionType { get; set; }

        public decimal CommisionRate { get; set; }

        public decimal CommisionValue { get; set; }
    }
}
