using System;

namespace OneCode.Draws.Dtos
{
    public class CreateDrawInputDto
    {
        public string Name { get; set; }

        public string Mobile { get; set; }

        public Guid OwnerId { get; set; }

        public Guid ShopId { get; set; }

        public decimal Amount { get; set; }
    }
}
