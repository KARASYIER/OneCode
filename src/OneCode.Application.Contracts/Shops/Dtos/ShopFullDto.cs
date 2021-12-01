using OneCode.BizImages.Dtos;
using OneCode.Salers.Dtos;
using OneCode.Tags.Dtos;
using System.Collections.Generic;

namespace OneCode.Shops.Dtos
{
    /// <summary>
    /// 完整店铺信息
    /// </summary>
    public class ShopFullDto : ShopDto
    {
        /// <summary>
        /// 店铺分销员
        /// </summary>
        public List<SalerDto> Salers { get; set; } = new List<SalerDto>();

        /// <summary>
        /// 
        /// </summary>
        public List<TagDto> Tags { get; set; } = new List<TagDto>();

        /// <summary>
        /// KV图列表
        /// </summary>
        //public List<BizImageDto> BizImages { get; set; } = new List<BizImageDto>();
    }
}
