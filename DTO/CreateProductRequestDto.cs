using System.ComponentModel.DataAnnotations;

namespace Telecom360.DTO.Product
{
    public class CreateProductRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }

        [MaxLength(50)]
        public string PriceModel { get; set; }
    }
}
