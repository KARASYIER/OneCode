using Newtonsoft.Json;
using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode.Shops.Dtos
{
    public class H5ProductDto
    {
        public string OutterId { get; set; }

        public ProductTypeEnum TypeId { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Url { get; set; }

        public string KvUrl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CityStart { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CityEnd { get; set; }
    }
}
