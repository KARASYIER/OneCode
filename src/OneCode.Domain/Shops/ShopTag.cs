using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace OneCode.Domain
{
    /// <summary>
    /// 店铺-标签 多对多映射表
    /// </summary>
    public class ShopTag : Entity
    {
        #region Property

        public virtual Guid ShopId { get; set; }

        public virtual Guid TagId { get; set; }

        public virtual Shop Shop { get; set; }

        public virtual Tag Tag { get; set; }

        #endregion

        #region Methods
        public override object[] GetKeys()
        {
            return new object[] { ShopId, TagId };
        }
        #endregion

        #region Ctor
        public ShopTag()
        {

        }

        public ShopTag(Guid shopId, Guid tagId)
        {
            ShopId = shopId;
            TagId = tagId;
        }
        #endregion

    }
}
