using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telecomm360.Model
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string KYCStatus { get; set; } = string.Empty;

        public string ContactInfo { get; set; } = string.Empty;

        // Optional: Navigation
        public ICollection<Subscriber>? Subscribers { get; set; }
    }
}

