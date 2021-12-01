using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace OneCode.Domain
{
    /// <summary>
    /// 产品表
    /// </summary>
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        #region Property

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 产品简介
        /// </summary>
        public virtual string Summary { get; set; }

        /// <summary>
        /// 产品分类（车票、酒店、车酒、度假）
        /// </summary>
        public virtual ProductTypeEnum TypeId { get; set; }

        /// <summary>
        /// 产品分类名称（车票、酒店、车酒、度假）
        /// </summary>
        public virtual string TypeName { get; set; }

        /// <summary>
        /// 产品来源(现在来源于微信，以后会有其他平台的产品)
        /// </summary>
        public virtual string SourceName { get; set; } = "微信";

        /// <summary>
        /// 产品url
        /// </summary>
        public virtual string Url { get; set; } = "";

        /// <summary>
        /// 产品KV图Url
        /// </summary>
        public virtual string KvUrl { get; set; }

        /// <summary>
        /// 产品价格
        /// </summary>
        public virtual decimal Price { get; set; }

        /// <summary>
        /// 佣金类型
        /// </summary>
        public virtual CommisionTypeEnum CommisionType { get; set; }

        /// <summary>
        /// 佣金率(产品字段默认佣金)
        /// </summary>
        public virtual decimal CommisionRate { get; set; }

        /// <summary>
        /// 固定佣金值
        /// </summary>
        public virtual decimal CommisionValue { get; set; }

        /// <summary>
        /// 产品销售状态（是否下架）
        /// </summary>
        public virtual bool IsOffShelf { get; set; }

        /// <summary>
        /// 产品库存状态（是否售罄）
        /// </summary>
        public virtual bool IsSellOut { get; set; }

        /// <summary>
        /// 产品排序
        /// </summary>
        public virtual int DisplayOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string CityStart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string CityEnd { get; set; }


        /// <summary>
        /// 外部编号(产品系统内编号)--新建产品后必须绑定外部编号才允许关联店铺
        /// 产品的定义粒度：
        /// 1）车票：以一条线路作为一个产品;
        /// 2）酒店：以一个酒店作为一个产品;
        /// 3）度假：以一个度假产品作为一个产品
        /// 4）车酒：以一个车酒包作为一个产品
        /// </summary>
        public virtual string OutterId { get; set; }
        #endregion

        #region Navigation Property

        /// <summary>
        /// 店铺与产品关联(多对多)
        /// </summary>
        public virtual ICollection<ShopProduct> ShopProducts { get; set; } = new List<ShopProduct>();
        #endregion

        #region Ctor
        public Product() { }

        public Product(Guid id) : base(id) { }
        #endregion
    }
}
