using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telecom360.Model;


namespace Telecomm360.Model
{
    public class ProvisioningTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProvisioningTaskId { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int SubscriberId { get; set; }
        public Subscriber Subscriber { get; set; }

        public string MSISDN { get; set; } = string.Empty;

        public string ResourceType { get; set; } = string.Empty;

        public string TargetSubsystem { get; set; } = string.Empty;

        public Status Status { get; set; }

    }
}