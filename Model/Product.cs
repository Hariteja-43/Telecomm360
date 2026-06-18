using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telecom360.Enum;

namespace Telecom360.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; } 
        public required string Name { get; set; }
        public required string Category { get; set; }
        public required string PriceModel { get; set; }
        public ProductStatus Status { get; set; }
    }
}