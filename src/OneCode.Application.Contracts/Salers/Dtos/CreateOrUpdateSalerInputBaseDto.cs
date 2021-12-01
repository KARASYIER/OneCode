using OneCode.EnumTypes;
using System;
using System.ComponentModel.DataAnnotations;

namespace OneCode.Salers.Dtos
{
    public class CreateOrUpdateSalerInputBaseDto
    {
        public string Mobile { get; set; }

        public string Name { get; set; }

        [Required]
        public Guid ShopId { get; set; }
    }
}
