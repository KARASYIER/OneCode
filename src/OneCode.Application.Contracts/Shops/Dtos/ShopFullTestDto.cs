using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Shops.Dtos
{
    public class ShopFullTestDto : ShopFullDto
    {
        public List<ShopProductDto> ShopProducts { get; set; }
    }
}
