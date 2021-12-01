using OneCode.Dtos;
using OneCode.EnumTypes;
using System;

namespace OneCode.Shops.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetShopsInputDto : PagedInputDto
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string filter { get; set; }

        /// <summary>
        /// 店铺负责人
        /// </summary>
        public Guid? OwnerId { get; set; }

        /// <summary>
        /// 店铺状态
        /// </summary>
        public ShopStatusEnum? ShopStatus { get; set; }
    }
}
