using System;

namespace OneCode.Products.Dtos
{
    public class UpdateRelatedShopsInputDto
    {
        public Guid ShopId { get; set; }

        public decimal CommisionRate { get; set; }

        public decimal CommisionValue { get; set; }
    }
}
