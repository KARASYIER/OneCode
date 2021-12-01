using System;

namespace OneCode.Shops.Dtos
{
    public class UpdateRelatedProductsInputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal CommisionRate { get; set; }
    }
}
