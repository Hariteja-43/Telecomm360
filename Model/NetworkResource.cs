using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telecomm360.Model
{
    public class NetworkResource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NetworkResourceId { get; set; }

        public string NetworkResourceType { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public int AllocatedTo { get; set; }   // SubscriberId (optional)

        public Status Status { get; set; }
    }
}