using Telecom360.DTO;
using Telecom360.Models;
using Telecom360.Repository.Interface;

namespace Telecom360.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> products = new();

        public async Task<List<Product>> GetAllProducts(ProductDto productDto) => products;

        public async Task<Product> GetProductById(int productId) =>
            products.FirstOrDefault(p => p.ProductId == productId);

        public async Task<Product> CreateProduct(Product product)
        {
            products.Add(product);
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var existing = products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existing == null) return null;

            existing.Name = product.Name;
            existing.Category = product.Category;
            existing.PriceModel = product.PriceModel;
            existing.Status = product.Status;

            return existing;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return false;

            product.Status = Enum.ProductStatus.ARCHIVED;
            return true;
        }
    }
}