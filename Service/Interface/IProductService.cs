using Telecom360.DTO;
using Telecom360.DTO.Product;

namespace Telecom360.Service.Interface
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProductById(int productId);
        Task<ProductDto> CreateProduct(CreateProductRequestDto request);
        Task<ProductDto> UpdateProduct(int productId, UpdateProductRequestDto request);
        Task<bool> DeleteProduct(int productId);
    }
}