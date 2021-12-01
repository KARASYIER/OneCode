using OneCode.EnumTypes;
using System;

namespace OneCode.BizImages.Dtos
{
    public class BizImageDto
    {
        /// <summary>
        /// 图片所属业务领域
        /// </summary>
        public BizImageScope BizScope { get; set; }

        /// <summary>
        /// 所属Id
        /// </summary>
        public Guid SubjectId { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 缩略图片地址
        /// </summary>
        public string ThumbUrl { get; set; }


        /// <summary>
        /// 原始图片地址
        /// </summary>
        public string OriginalUrl { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
