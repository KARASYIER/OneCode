using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Orders.Dtos
{
    public class CreateOrderResultDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public decimal TotalAmount { get; set; }

        public string OutterOrderId { get; set; }

        public string CreationTime { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
