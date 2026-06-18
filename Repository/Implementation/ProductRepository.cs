using Telecom360.DTO.Product;
using Telecom360.Model;
using Telecom360.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Telecom360.DTO;
using Telecomm360.Data; 

namespace Telecom360.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(int productId)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }


        public async Task<Product?> UpdateProduct(Product product)
        {
            var existing = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

            if (existing == null)
                return null;

            if (!string.IsNullOrWhiteSpace(product.Name))
                existing.Name = product.Name;

            if (!string.IsNullOrWhiteSpace(product.Category))
                existing.Category = product.Category;

            if (!string.IsNullOrWhiteSpace(product.PriceModel))
                existing.PriceModel = product.PriceModel;


            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
                return false;

            _context.Products.Remove(product);

            var rows = await _context.SaveChangesAsync();

            return rows > 0;
        }
    }
}
