

namespace Telecom360.DTO.Product
{
    public class CreateProductResponseDto
    {
        public int ProductID { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string PriceModel { get; set; }

        public string Status { get; set; }

        public string Message { get; set; }
    }
}