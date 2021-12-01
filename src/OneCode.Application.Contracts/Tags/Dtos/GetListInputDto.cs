using OneCode.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Tags.Dtos
{
    public class GetListInputDto : PagedInputDto
    {
        public string Filter { get; set; }
    }
}
