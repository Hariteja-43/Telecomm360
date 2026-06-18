namespace Telecom360.DTO
{
    public class OrderDto
    {
        public int OrderID { get; set; }
        public int SubscriberID { get; set; }
        public int ProductID { get; set; }
        public required string Status { get; set; }
    }
}