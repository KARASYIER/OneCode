using System;

namespace OneCode.Shops.Dtos
{
    public class GetShopCommisionDetailsDto
    {
        public string Title { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalCommision { get; set; }

        public string CreationTime { get; set; }
    }
}
