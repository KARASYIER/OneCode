using System.Collections.Generic;

namespace OneCode.Orders.Dtos
{
    public class OrderFullDto : OrderDto
    {

        public List<OrderDetailDto> Details { get; set; }
    }
}
