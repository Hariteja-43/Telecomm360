using System.ComponentModel.DataAnnotations;

namespace Telecom360.DTO.Order
{
    public class CreateOrderRequestDto
    {
        [Required]
        public int SubscriberID { get; set; }

        [Required]
        public int ProductID { get; set; }
    }
}