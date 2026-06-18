using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Telecomm360.Model
{
    public class Subscriber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubscriberId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public string MSISDN { get; set; } = string.Empty;

        public string IMSI { get; set; } = string.Empty;

        public Status SIMStatus { get; set; }

        public string DeviceId { get; set; } = string.Empty;

        public Status Status { get; set; }

        // Navigation to the Customer Table [customerId is the foreign key in this table]
        public Customer? Customer { get; set; }
    }
}