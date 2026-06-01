namespace Telecom360.DTO.Product
{
    public class ProductResponseDto
    {
         public int ProductID { get; set; }
        public required string Name { get; set; }
        public required string Category { get; set; }
        public required string PriceModel { get; set; }
        public required string Status { get; set; }
    }
}
