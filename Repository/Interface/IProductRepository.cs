using Telecom360.DTO;
using Telecom360.DTO.Product;
using Telecom360.Models;

namespace Telecom360.Repository.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts(ProductDto productDto);
        Task<Product> GetProductById(int productId);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
       
    }
}