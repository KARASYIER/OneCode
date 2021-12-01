using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace OneCode.Domain
{
    /// <summary>
    /// 店铺
    /// </summary>
    public class Shop : FullAuditedAggregateRoot<Guid>
    {
        #region Property

        /// <summary>
        /// 店铺名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 店铺简介
        /// </summary>
        public virtual string Summary { get; set; }

        /// <summary>
        /// 店铺详细介绍
        /// </summary>
        public virtual string Description { get; set; }


        /// <summary>
        /// 店铺Logo
        /// url
        /// </summary>
        public virtual string Logo { get; set; }


        /// <summary>
        /// 店铺主KV图
        /// url
        /// </summary>
        public virtual string Kv { get; set; }


        /// <summary>
        /// 店铺联系电话
        /// </summary>
        public virtual string Telephone { get; set; }


        /// <summary>
        /// 店铺地址
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// 店铺所在经度
        /// </summary>
        public virtual string Longitude { get; set; }

        /// <summary>
        /// 店铺所在的纬度
        /// </summary>
        public virtual string Latitude { get; set; }

        /// <summary>
        /// 二维码图片地址
        /// </summary>
        public virtual string QRCodeUrl { get; set; }

        /// <summary>
        /// 店铺负责人Id
        /// </summary>
        public virtual Guid? OwnerId { get; set; }

        /// <summary>
        /// 店铺负责人名称
        /// </summary>
        public virtual string OwnerName { get; set; }


        /// <summary>
        /// 店铺前台展示模板
        /// </summary>
        public virtual ShopTemplateTypeEnum Template { get; set; } = ShopTemplateTypeEnum.Default;


        /// <summary>
        /// 是否在该店铺中显示官方信息
        /// </summary>
        public virtual bool IsShowOfficialLogo { get; set; }

        /// <summary>
        /// 店铺状态 
        /// </summary>
        public virtual ShopStatusEnum Status { get; set; } = ShopStatusEnum.Openning;


        /// <summary>
        /// 店铺默认佣金比例
        /// </summary>
        //public virtual decimal CommisionRate { get; set; }


        /// <summary>
        /// 当前可用佣金金额(可提现的佣金)
        /// </summary>
        public virtual decimal CommisionAvailable { get; set; } = 0.00M;

        /// <summary>
        /// 正在申请提现中的佣金(该值大于0时不可申请提现)
        /// </summary>
        public virtual decimal CommisionApplying { get; set; } = 0.00M;

        /// <summary>
        /// 订单进行中的佣金(未完成,不可提现)
        /// </summary>
        public virtual decimal CommisionDoing { get; set; } = 0.00M;

        #endregion

        #region Navigation Property
        /// <summary>
        /// 店铺分销员
        /// </summary>
        public virtual ICollection<Saler> Salers { get; set; } = new List<Saler>();

        /// <summary>
        /// 店铺标签
        /// </summary>
        public virtual ICollection<ShopTag> ShopTags { get; set; } = new List<ShopTag>();

        /// <summary>
        /// 店铺产品
        /// </summary>
        public virtual ICollection<ShopProduct> ShopProducts { get; set; } = new List<ShopProduct>();


        public virtual ICollection<Product> Products { get; set; } = new List<Product>();


        #endregion

        #region ctor

        public Shop() { }

        public Shop(Guid id) : base(id) { }
        #endregion
    }

}
