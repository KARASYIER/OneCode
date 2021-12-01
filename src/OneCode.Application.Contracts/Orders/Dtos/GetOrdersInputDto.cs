using OneCode.Dtos;
using OneCode.EnumTypes;
using System;

namespace OneCode.Orders.Dtos
{
    public class GetOrdersInputDto : PagedInputDto
    {

        public string Filter { get; set; }

        public string OutterOrderId { get; set; }

        public Guid? ShopId { get; set; }

        public Guid? SalerId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public OrderStatus? OrderStatus { get; set; }

        public OrderBizStatus? BizStatus { get; set; }
    }
}
