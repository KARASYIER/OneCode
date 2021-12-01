using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneCode.ViewModels
{
    public class ProductBusViewModel
    {
        public int ProductionId { get; set; }
        public string ProductionName { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FromCity { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ToCity { get; set; }
    }
}
