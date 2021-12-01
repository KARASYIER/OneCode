using OneCode.EnumTypes;

namespace OneCode.Products.Dtos
{
    /// <summary>
    /// 新增产品,此属性新增后无法修改
    /// </summary>
    public class CreateProductInputDto : CreateOrUpdateProductInputBaseDto
    {

        /// <summary>
        /// 外部编号
        /// </summary>
        public string OutterId { get; set; }

        /// <summary>
        /// 出发城市
        /// </summary>
        public string CityStart { get; set; }

        /// <summary>
        /// 到达城市
        /// </summary>
        public string CityEnd { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 产品分类（车票、酒店、车酒、度假）
        /// </summary>
        public ProductTypeEnum TypeId { get; set; }

        /// <summary>
        /// 产品来源(现在来源于微信，以后会有其他平台的产品)
        /// </summary>
        public string SourceName { get; set; } = "微信";

    }
}
