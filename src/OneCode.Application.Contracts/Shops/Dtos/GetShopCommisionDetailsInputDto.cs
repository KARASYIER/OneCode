using System;

namespace OneCode.Shops.Dtos
{
    public class GetShopCommisionDetailsInputDto
    {
        public Guid? SalerId { get; set; }

        public DateTime? Date { get; set; }
    }
}
