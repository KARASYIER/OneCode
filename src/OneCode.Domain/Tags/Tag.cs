using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace OneCode.Domain
{
    public class Tag : FullAuditedAggregateRoot<Guid>
    {
        #region Property

        public virtual string Name { get; set; }

        public virtual ICollection<ShopTag> ShopTags { get; set; } = new List<ShopTag>();

        #endregion

        #region Ctor
        public Tag() { }

        public Tag(Guid id) : base(id) { }

        #endregion
    }
}
