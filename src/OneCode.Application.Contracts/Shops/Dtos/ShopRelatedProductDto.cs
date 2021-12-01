using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Shops.Dtos
{
    public class ShopRelatedProductDto
    {        /// <summary>
             /// ID
             /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 佣金率(产品字段默认佣金)
        /// </summary>
        public decimal OriginCommisionRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CommisionTypeEnum CommisionType { get; set; }

        /// <summary>
        /// 实际佣金率
        /// </summary>
        public decimal CommisionRate { get; set; }

        /// <summary>
        /// 佣金金额
        /// </summary>
        public decimal CommisionValue { get; set; }

        /// <summary>
        /// 产品排序
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
