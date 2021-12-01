using OneCode.Shops.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Products.Dtos
{
    public class ProductAllShopDto : ProductDto
    {
        /// <summary>
        /// 店铺集合
        /// </summary>
        public List<ShopDto> Shops { get; set; }
    }
}
