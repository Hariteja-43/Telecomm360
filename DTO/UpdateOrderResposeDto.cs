namespace Telecom360.DTO.Order
{
    public class OrderActionResponseDto
    {
        public int OrderID { get; set; }

        public required string Status { get; set; }

        public required string Message { get; set; }

        // public DateTime ActionTimestamp { get; set; }
    }
}