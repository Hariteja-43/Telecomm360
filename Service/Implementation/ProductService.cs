using Telecom360.Service.Interface;
using Telecom360.Repository.Interface;
using Telecom360.DTO.Product;
using Telecom360.Model;
using Telecom360.DTO;

namespace Telecom360.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        // GET ALL PRODUCTS
        public async Task<List<ProductResponseDto>> GetAllProducts()
        {
            var products = await _repo.GetAllProducts();

            if (products == null || !products.Any())
                return new List<ProductResponseDto>();

            return products.Select(MapToDto).ToList();
        }

        // GET PRODUCT BY ID
        public async Task<ProductResponseDto?> GetProductById(int productId)
        {
            var product = await _repo.GetProductById(productId);

            return product == null ? null : MapToDto(product);
        }

        // CREATE PRODUCT
        public async Task<ProductResponseDto?> CreateProduct(CreateProductRequestDto request)
        {
            if (request == null)
                return null;

            var product = new Product
            {
                Name = request.Name,
                Category = request.Category,
                PriceModel = request.PriceModel
            };

            var created = await _repo.CreateProduct(product);

            return created == null ? null : MapToDto(created);
        }

        // UPDATE PRODUCT
        public async Task<ProductResponseDto?> UpdateProduct(int productId, UpdateProductRequestDto request)
        {
            if (request == null)
                return null;

            var existing = await _repo.GetProductById(productId);

            if (existing == null)
                return null;

            existing.Name = request.Name ?? existing.Name;
            existing.Category = request.Category ?? existing.Category;
            existing.PriceModel = request.PriceModel ?? existing.PriceModel;

            var updated = await _repo.UpdateProduct(existing);

            return updated == null ? null : MapToDto(updated);
        }

        // ✅ HARD DELETE (FIXED)
        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _repo.GetProductById(productId);

            if (product == null)
                return false;

            return await _repo.DeleteProduct(productId); // ✅ FIXED HERE
        }

        // DTO MAPPING
        private static ProductResponseDto MapToDto(Product product)
        {
            return new ProductResponseDto
            {
                ProductID = product.ProductId,
                Name = product.Name,
                Category = product.Category,
                PriceModel = product.PriceModel,
                Status = product.Status.ToString()
            };
        }
    }
}