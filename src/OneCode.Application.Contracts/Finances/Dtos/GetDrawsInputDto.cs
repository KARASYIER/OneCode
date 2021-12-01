using OneCode.Dtos;
using OneCode.EnumTypes;
using System;

namespace OneCode.Draws.Dtos
{
    public class GetDrawsInputDto : PagedInputDto
    {
        public string Filter { get; set; }

        public Guid? ShopId { get; set; }

        public DrawStatusEnum? Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
