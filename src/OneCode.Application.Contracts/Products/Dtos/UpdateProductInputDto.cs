using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Products.Dtos
{
    public class UpdateProductInputDto: CreateOrUpdateProductInputBaseDto
    {
        public bool CoverCommision { get; set; } = false; 
    }
}
