using OneCode.EnumTypes;
using OneCode.Tags.Dtos;
using System;
using System.Collections.Generic;

namespace OneCode.Shops.Dtos
{
    /// <summary>
    /// 店铺
    /// </summary>
    public class ShopDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

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

        public string QRCodeUrl { get; set; }


        /// <summary>
        /// 店铺前台展示模板
        /// </summary>
        public ShopTemplateTypeEnum Template { get; set; }


        /// <summary>
        /// 是否在该店铺中显示官方信息
        /// </summary>
        public bool IsShowOfficialLogo { get; set; }

        /// <summary>
        /// 店铺状态 
        /// </summary>
        public ShopStatusEnum Status { get; set; }

        /// <summary>
        /// 当前可用佣金金额(可提现的佣金)
        /// </summary>
        public decimal CommisionAvailable { get; set; }

        /// <summary>
        /// 正在申请提现中的佣金(该值大于0时不可申请提现)
        /// </summary>
        public decimal CommisionApplying { get; set; }

        /// <summary>
        /// 订单进行中的佣金(未完成,不可提现)
        /// </summary>
        public decimal CommisionDoing { get; set; }

        /// <summary>
        /// 标签集合
        /// </summary>
        //public List<TagDto> Tags { get; set; }
    }
}
