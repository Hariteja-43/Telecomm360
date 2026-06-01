using Telecom360.Services.Interface;
using Telecom360.Repository.Interface;
using Telecom360.DTO.Product;
using Telecom360.Models;
using Telecom360.Enum;
using Telecom360.DTO;

namespace Telecom360.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        // GET ALL PRODUCTS
        //Task<List<ProductResponseDto>> GetAllProducts(ProductDto productDto);

        public async Task<List<ProductResponseDto>> GetAllProducts(ProductDto productDto)
{
    // ✅ Call repository (should return List<Product>)
    var products = await _repo.GetAllProducts(productDto);

    if (products == null || !products.Any())
        return new List<ProductResponseDto>();

    // ✅ Convert Entity → DTO and return List
    return products.Select(p => new ProductResponseDto
    {
        ProductID = p.ProductId,      // ✅ Correct property name
        Name = p.Name,
        Category = p.Category,
        PriceModel = p.PriceModel,
        Status = p.Status.ToString()
    }).ToList();                     // ✅ IMPORTANT: Convert to List
}

        //  GET PRODUCT BY ID
        //Task<ProductResponseDto> GetProductById(int productId);

        public async Task<ProductResponseDto> GetProductById(int productId)
        {
            var product = await _repo.GetProductById(productId);

            if (product == null) return null;

            return new ProductResponseDto
            {
                ProductID = product.ProductId,
                Name = product.Name,
                Category = product.Category,
                PriceModel = product.PriceModel,
                Status = product.Status.ToString()
            };
        }

        // CREATE PRODUCT
        //Task<ProductResponseDto> CreateProduct(ProductDto dto);
        public async Task<ProductResponseDto> CreateProduct(CreateProductRequestDto request)
        {
            var product = new Product
            { // Will be assigned by the repository
                Name = request.Name,
                Category = request.Category,
                PriceModel = request.PriceModel,
                Status = ProductStatus.ACTIVE
            };

            var created = await _repo.CreateProduct(product);

            return new ProductResponseDto
            {
                ProductID = created.ProductId,
                Name = created.Name,
                Category = created.Category,
                PriceModel = created.PriceModel,
                Status = created.Status.ToString()
            };
        }

        // UPDATE PRODUCT
        //Task<ProductResponseDto> UpdateProduct(int productId, ProductDto dto);
        public async Task<ProductResponseDto?> UpdateProduct(int productId, UpdateProductRequestDto request)
        {
            var existing = await _repo.GetProductById(productId);
            if (existing == null) return null;

            // ✅ Update allowed fields
            existing.Name = request.Name ?? existing.Name;
            existing.Category = request.Category ?? existing.Category;
            existing.PriceModel = request.PriceModel ?? existing.PriceModel;

            // ✅ Safe enum parsing
            // if (!string.IsNullOrWhiteSpace(request.Status) &&
            //     TryParse.ToEnum<ProductStatus>(request.Status, out var status))
            // {
            //     existing.Status = status;
            // }

            var updated = await _repo.UpdateProduct(existing);

            return new ProductResponseDto
            {
                ProductID = updated.ProductId,
                Name = updated.Name,
                Category = updated.Category,
                PriceModel = updated.PriceModel,
                Status = updated.Status.ToString()
            };
        }

        // ✅ ARCHIVE PRODUCT (DELETE)
        public async Task<bool> DeleteProduct(int productId)
        {
            var existing = await _repo.GetProductById(productId);
            if (existing == null) return false;

            // ✅ Soft delete (archive)
            existing.Status = ProductStatus.ARCHIVED;

            await _repo.UpdateProduct(existing);

            return true;
        }
    }
}
