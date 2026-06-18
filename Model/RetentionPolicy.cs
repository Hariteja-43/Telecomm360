using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telecom360.Models
{
    public class RetentionPolicy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PolicyID { get; set; }
        public required string DataType { get; set; }
        public int RetentionPeriod { get; set; }
        public DateTime AppliedFrom { get; set; }
    }
}