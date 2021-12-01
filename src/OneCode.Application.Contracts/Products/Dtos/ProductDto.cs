using OneCode.EnumTypes;
using System;

namespace OneCode.Products.Dtos
{
    /// <summary>
    /// H5页面展示的产品列表
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 产品简介
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 产品分类（车票、酒店、车酒、度假）
        /// </summary>
        public ProductTypeEnum TypeId { get; set; }

        /// <summary>
        /// 产品分类名称（车票、酒店、车酒、度假）
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 产品来源(现在来源于微信，以后会有其他平台的产品)
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// 产品url
        /// </summary>
        //public string Url { get; set; }

        /// <summary>
        /// 产品KV图Url
        /// </summary>
        public string KvUrl { get; set; }

        /// <summary>
        /// 产品价格
        /// </summary>
        //public decimal Price { get; set; }

        /// <summary>
        /// 佣金类型
        /// </summary>
        public CommisionTypeEnum CommisionType { get; set; }

        /// <summary>
        /// 佣金率(产品字段默认佣金)
        /// </summary>
        public decimal CommisionRate { get; set; }
        
        /// <summary>
        /// 佣金额
        /// </summary>
        public decimal CommisionValue { get; set; }

        /// <summary>
        /// 产品销售状态（是否下架）
        /// </summary>
        public bool IsOffShelf { get; set; }

        /// <summary>
        /// 产品库存状态（是否售罄）
        /// </summary>
        public bool IsSellOut { get; set; }

        /// <summary>
        /// 产品排序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 外部编号(产品系统内编号)--新建产品后必须绑定外部编号才允许关联店铺
        /// 产品的定义粒度：1）车票：以一条线路作为一个产品;2）酒店：以一个酒店作为一个产品;
        /// 3）度假：以一个度假产品作为一个产品4）车酒：以一个车酒包作为一个产品
        /// </summary>
        public string OutterId { get; set; }

        public string CityStart { get; set; }

        public string CityEnd { get; set; }
    }
}
