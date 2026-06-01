using Telecom360.DTO;
using Telecom360.DTO.Product;

namespace Telecom360.Services.Interface
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllProducts(ProductDto productDto);
        Task<ProductResponseDto> GetProductById(int productId);
        Task<ProductResponseDto> CreateProduct(CreateProductRequestDto request);
        Task<ProductResponseDto> UpdateProduct(int productId, UpdateProductRequestDto request);
        Task<bool> DeleteProduct(int productId);
    }
}