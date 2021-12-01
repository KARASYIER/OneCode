using OneCode.EnumTypes;

namespace OneCode.Orders.Dtos
{
    public class CreateOrderDetailInputDto
    {
        /// <summary>
        /// 外部产品Id
        /// </summary>
        public string OutterProductId { get; set; }


        /// <summary>
        /// 产品分类（车票、酒店、车酒、度假）
        /// </summary>
        public ProductTypeEnum ProductType { get; set; }

        /// <summary>
        /// 产品标题
        /// </summary>
        public string ProductTitle { get; set; }

        /// <summary>
        /// 产品金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 利润
        /// </summary>
        //public decimal Profit { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
