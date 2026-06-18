using Microsoft.AspNetCore.Mvc;
using Telecom360.Service.Interface;
using Telecom360.Constant;
using Telecom360.DTO.Product;

namespace Telecom360.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        
[HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _service.GetAllProducts();

                if (products == null)
                {
                    products = new List<ProductDto>();
                }

                return Ok(products);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (id <= 0) return BadRequest(ErrorMessages.INVALID_ID);
            try
            {
                var data = await _service.GetProductById(id);
               return data == null ? NotFound(ErrorMessages.NOT_FOUND) : Ok(data);

            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.CreateProduct(request);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequestDto request)
        {
            if (id <= 0) return BadRequest(ErrorMessages.INVALID_ID);
            try
            {
                var result = await _service.UpdateProduct(id, request);
                return result == null ? NotFound(ErrorMessages.NOT_FOUND)  : Ok(result);

            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0) return BadRequest(ErrorMessages.INVALID_ID);
            try
            {
                var success = await _service.DeleteProduct(id);
                return !success ? NotFound(ErrorMessages.NOT_FOUND) : Ok();
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }
    }
}