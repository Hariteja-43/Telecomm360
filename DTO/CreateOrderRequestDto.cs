using System.ComponentModel.DataAnnotations;

namespace Telecom360.DTO.Order
{
    public class CreateOrderRequestDto
    {
        [Required]
        public int SubscriberID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public required string Status { get; set; }
        [Required]
        public required string FulfillmentSteps { get; set; }

        // SubscriberID = request.SubscriberID,
        //         ProductID = request.ProductID,
        //         OrderDate = DateTime.UtcNow,
        //         Status = "CREATED",
        //         FulfillmentSteps = "INIT"
    }
}
