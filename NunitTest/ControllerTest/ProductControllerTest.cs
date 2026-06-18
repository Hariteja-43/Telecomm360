using NUnit.Framework;
using Moq;
using Telecom360.Controllers;
using Telecom360.Service.Interface;
using Telecom360.DTO.Product;
using Telecom360.DTO;
using Telecom360.Constant;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Telecom360.Test.ControllerTest
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductService> _serviceMock;
        private ProductController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IProductService>();
            _controller = new ProductController(_serviceMock.Object);
        }

        // helper
        private ProductResponseDto CreateProduct(int id = 1)
        {
            return new ProductResponseDto
            {
                ProductID = id,
                Name = "Test Product",
                Category = "Electronics",
                PriceModel = "Fixed",
                Status = "Active"
            };
        }

        // ---------------- GET ALL ----------------

        [Test]
        public async Task GetAllProducts_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllProducts())
                        .ReturnsAsync(new List<ProductResponseDto>
                        {
                            CreateProduct(1),
                            CreateProduct(2)
                        });

            var result = await _controller.GetAllProducts();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllProducts_Empty_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllProducts())
                        .ReturnsAsync(new List<ProductResponseDto>());

            var result = await _controller.GetAllProducts();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllProducts_ServiceCalled()
        {
            _serviceMock.Setup(s => s.GetAllProducts())
                        .ReturnsAsync(new List<ProductResponseDto>());

            await _controller.GetAllProducts();

            _serviceMock.Verify(s => s.GetAllProducts(), Times.Once);
        }

        // ---------------- GET BY ID ----------------

        [Test]
        public async Task GetProductById_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetProductById(1))
                        .ReturnsAsync(CreateProduct());

            var result = await _controller.GetProductById(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetProductById_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.GetProductById(1))
                        .ReturnsAsync((ProductResponseDto?)null);

            var result = await _controller.GetProductById(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetProductById_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.GetProductById(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        // ---------------- CREATE ----------------

        [Test]
        public async Task CreateProduct_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CreateProduct(It.IsAny<CreateProductRequestDto>()))
                        .ReturnsAsync(CreateProduct());

            var result = await _controller.CreateProduct(new CreateProductRequestDto());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateProduct_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("x", "invalid");

            var result = await _controller.CreateProduct(new CreateProductRequestDto());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task CreateProduct_ServiceCalled()
        {
            _serviceMock.Setup(s => s.CreateProduct(It.IsAny<CreateProductRequestDto>()))
                        .ReturnsAsync(CreateProduct());

            await _controller.CreateProduct(new CreateProductRequestDto());

            _serviceMock.Verify(s => s.CreateProduct(It.IsAny<CreateProductRequestDto>()), Times.Once);
        }

        // ---------------- UPDATE ----------------

        [Test]
        public async Task UpdateProduct_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.UpdateProduct(1, It.IsAny<UpdateProductRequestDto>()))
                        .ReturnsAsync(CreateProduct());

            var result = await _controller.UpdateProduct(1, new UpdateProductRequestDto());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task UpdateProduct_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.UpdateProduct(1, It.IsAny<UpdateProductRequestDto>()))
                        .ReturnsAsync((ProductResponseDto?)null);

            var result = await _controller.UpdateProduct(1, new UpdateProductRequestDto());

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task UpdateProduct_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.UpdateProduct(0, new UpdateProductRequestDto());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        // ---------------- DELETE ----------------

        [Test]
        public async Task DeleteProduct_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.DeleteProduct(1))
                        .ReturnsAsync(true);

            var result = await _controller.DeleteProduct(1);

            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public async Task DeleteProduct_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.DeleteProduct(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task DeleteProduct_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.DeleteProduct(1))
                        .ReturnsAsync(false);

            var result = await _controller.DeleteProduct(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }
    }
}
