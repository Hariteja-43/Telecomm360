using Telecom360.DTO;
using Telecom360.Model;

namespace Telecom360.Repository.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();

        Task<Product?> GetProductById(int productId);

        Task<Product> CreateProduct(Product product);

        Task<Product?> UpdateProduct(Product product);

        Task<bool> DeleteProduct(int productId);
    }
}