using OneCode.Dtos;
using OneCode.EnumTypes;
using System;

namespace OneCode.Products.Dtos
{
    public class GetProductsInputDto : PagedInputDto
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public ProductTypeEnum? ProductType { get; set; }

        /// <summary>
        /// 产品销售状态(是否下架)
        /// </summary>
        public bool? IsOffShelf { get; set; }

        /// <summary>
        /// 产品库存状态（是否售罄）
        /// </summary>
        public bool? IsSellOut { get; set; }
    }
}
