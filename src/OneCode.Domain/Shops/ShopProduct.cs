using OneCode.EnumTypes;
using System;
using Volo.Abp.Domain.Entities;

namespace OneCode.Domain
{
    /// <summary>
    /// 店铺-产品多对多关联表
    /// </summary>
    public class ShopProduct : Entity
    {
        #region Property

        public virtual Guid ShopId { get; set; }

        public virtual Guid ProductId { get; set; }

        public virtual Shop Shop { get; set; }

        public virtual Product Product { get; set; }

        /// <summary>
        /// 佣金类型
        /// </summary>
        public virtual decimal CommisionValue { get; set; }

        /// <summary>
        /// 佣金额
        /// </summary>
        public virtual CommisionTypeEnum CommisionType { get; set; }

        /// <summary>
        /// 佣金比例
        /// </summary>
        public virtual decimal CommisionRate { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        public virtual int DisplayOrder { get; set; }

        #endregion

        public override object[] GetKeys()
        {
            return new object[] { ShopId, ProductId };
        }

        #region Ctor
        public ShopProduct()
        {

        }

        public ShopProduct(Guid shopId, Guid productId)
        {
            ShopId = shopId;
            ProductId = productId;
        }
        #endregion
    }
}
