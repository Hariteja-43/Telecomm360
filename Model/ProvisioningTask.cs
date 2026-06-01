using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Telecomm360.Model
{
    public class ProvisioningTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        public string OrderId { get; set; } = string.Empty;

        [ForeignKey("Subscriber")]
        public int SubscriberId { get; set; }

        public string MSISDN { get; set; } = string.Empty;

        public string ResourceType { get; set; } = string.Empty;

        public string TargetSubsystem { get; set; } = string.Empty;

        public Status Status { get; set; }

        // Navigation to the Subscriber Table [subscriberId is the foreign key in this table]
        public Subscriber? Subscriber { get; set; }
    }
}