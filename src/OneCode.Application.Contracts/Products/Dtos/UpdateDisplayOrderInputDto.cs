using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Products.Dtos
{
    public class UpdateProductDisplayOrderInputDto
    {
        public Guid Changing { get; set; }

        public Guid Target { get; set; }
    }
}
