namespace Telecom360.DTO.Order
{
    public class OrderResponseDto
    {
        public int OrderID { get; set; }

        public int SubscriberID { get; set; }

        public int ProductID { get; set; }

        public DateTime OrderDate { get; set; }

        public required string Status { get; set; }

        public required  string FulfillmentSteps { get; set; }
    }
}
