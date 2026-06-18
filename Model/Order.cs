using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telecomm360.Model;

namespace Telecom360.Model
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public int SubscriberID { get; set; } // Foreign key to Subscriber
        public Subscriber Subscriber { get; set; } 
        public int ProductID { get; set; }  // Foreign key to Product
        public Product Product{ get; set; } 
        public DateTime OrderDate { get; set; }
        public required string Status { get; set; }
        public required string FulfillmentSteps { get; set; }

        // Order(OrderID, SubscriberID, ProductID, OrderDate, Status, FulfillmentSteps)
    }
}