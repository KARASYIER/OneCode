using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Orders.Dtos
{
    public class RefundFullOrderInputDto
    {
        public string OutterOrderId { get; set; }

        public decimal TotalAmount { get; set; }

    }
}
