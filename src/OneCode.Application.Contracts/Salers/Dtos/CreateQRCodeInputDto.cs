using System;

namespace OneCode.Salers.Dtos
{
    public class CreateQRCodeInputDto
    {
        public Guid ShopId { get; set; }

        public Guid SalerId { get; set; }
    }
}
