using Telecom360.DTO;
using Telecom360.DTO.Product;

namespace Telecom360.Service.Interface
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllProducts();
        Task<ProductResponseDto> GetProductById(int productId);
        Task<ProductResponseDto> CreateProduct(CreateProductRequestDto request);
        Task<ProductResponseDto> UpdateProduct(int productId, UpdateProductRequestDto request);
        Task<bool> DeleteProduct(int productId);
    }
}