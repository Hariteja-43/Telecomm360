namespace Telecom360.DTO.Order
{
    public class UpdateOrderResponseDto
    {
        public int OrderID { get; set; }

        public int SubscriberID { get; set; }

        public int ProductID { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public string FulfillmentSteps { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Message { get; set; }
    }
}
