using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Products.Dtos
{
    public class CreateOrUpdateProductInputBaseDto
    {
        /// <summary>
        /// 产品简介
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 产品KV图Url
        /// </summary>
        public string KvUrl { get; set; }

        /// <summary>
        /// 产品价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 佣金率(产品字段默认佣金)
        /// </summary>
        public decimal CommisionRate { get; set; }

        /// <summary>
        /// 佣金值
        /// </summary>
        public decimal CommisionValue { get; set; }
    }
}
