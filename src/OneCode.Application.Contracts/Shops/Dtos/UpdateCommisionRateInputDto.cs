using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Shops.Dtos
{
    public class UpdateCommisionRateInputDto
    {
        public Guid ProductId { get; set; }
        public decimal CommisionRate { get; set; }

        public decimal CommisionValue { get; set; }
    }
}
