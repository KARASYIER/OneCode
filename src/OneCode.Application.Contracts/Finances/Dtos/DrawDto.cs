
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OneCode.EnumTypes;
using System;

namespace OneCode.Draws.Dtos
{
    public class DrawDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public Guid ShopId { get; set; }

        public string ShopName { get; set; }

        public decimal Amount { get; set; }

        public DrawStatusEnum DrawStatus { get; set; }

        public string CreationTime { get; set; }

        public string ConfirmTime { get; set; }
    }

}
