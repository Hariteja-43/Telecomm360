using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telecom360.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public int SubscriberID { get; set; }
        public int ProductID { get; set; }
        // Navigation properties
        public Product Product{ get; set; }
        public DateTime OrderDate { get; set; }
        public required string Status { get; set; }
        public required string FulfillmentSteps { get; set; }
    }
}