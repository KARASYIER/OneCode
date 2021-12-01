using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Shops.Dtos
{
    public class H5ShopWithProductsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string Logo { get; set; }

        public string Address { get; set; }

        public bool IsShowOfficialLogo { get; set; }

        public List<H5ProductDto> Products { get; set; }
    }
}
