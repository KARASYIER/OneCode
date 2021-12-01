using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Shops.Dtos
{
    public class UpdateDisplayOrderInputDto
    {
        public Guid Changing { get; set; }

        public Guid Target { get; set; }
    }
}
