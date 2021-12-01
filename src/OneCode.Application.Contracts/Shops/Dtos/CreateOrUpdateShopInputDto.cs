using OneCode.EnumTypes;
using System;
using System.Collections.Generic;

namespace OneCode.Shops.Dtos
{
    /// <summary>
    /// 新增/修改店铺的数据模型
    /// </summary>
    public class CreateOrUpdateShopInputDto
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 店铺简介
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 店铺详细介绍
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 店铺Logo
        /// url
        /// </summary>
        public string Logo { get; set; }


        /// <summary>
        /// 店铺主KV图
        /// url
        /// </summary>
        public string Kv { get; set; }


        /// <summary>
        /// 店铺联系电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 店铺地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 店铺所在经度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 店铺所在的纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 店铺负责人Id
        /// </summary>
        public Guid? OwnerId { get; set; }

        /// <summary>
        /// 店铺负责人名称
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// 是否在该店铺中显示官方信息
        /// </summary>
        public bool IsShowOfficialLogo { get; set; }

        /// <summary>
        /// 标签集合
        /// </summary>
        public List<TagInputDto> tags { get; set; }

    }
}
