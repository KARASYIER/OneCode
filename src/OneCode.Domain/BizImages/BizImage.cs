using OneCode.EnumTypes;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace OneCode.Domain
{
    /// <summary>
    /// 系统中业务领域中使用的图片
    /// </summary>
    public class BizImage : FullAuditedAggregateRoot<Guid>
    {
        #region Property

        /// <summary>
        /// 图片所属业务领域
        /// </summary>
        public virtual BizImageScope BizScope { get; set; }

        /// <summary>
        /// 所属Id
        /// </summary>
        public virtual Guid SubjectId { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        /// 缩略图片地址
        /// </summary>
        public virtual string ThumbUrl { get; set; }


        /// <summary>
        /// 原始图片地址
        /// </summary>
        public virtual string OriginalUrl { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        public virtual int DisplayOrder { get; set; }

        #endregion

        #region Ctor
        public BizImage()
        {

        }

        public BizImage(Guid id, string url, int displayOrder)
        {
            this.Id = id;
            this.Url = url;
            this.DisplayOrder = displayOrder;
        }
        #endregion

    }
}
