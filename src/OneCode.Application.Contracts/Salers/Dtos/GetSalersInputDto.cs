using OneCode.Dtos;
using OneCode.EnumTypes;
using System;

namespace OneCode.Salers.Dtos
{
    public class GetSalersInputDto : PagedInputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? ShopId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SalerTypeEnum? Type { get; set; }


    }
}
