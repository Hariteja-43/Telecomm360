using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Telecom360.DTO.Product;
using Telecom360.Service.Interface;
using Telecom360.Controllers;

namespace Telecomm360.Test.ControllerTest
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

        #region Helpers

        private ProductDto CreateProduct(int id = 1)
        {
            return new ProductDto
            {
                ProductID = id,
                Name = "Test Product",
                Category = "Electronics",
                PriceModel = "Fixed",
                Status = "Active"
            };
        }

        #endregion

        #region GET ALL

        [Test]
        public async Task GetAllProducts_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllProducts())
                        .ReturnsAsync(new List<ProductDto> { CreateProduct(), CreateProduct(2) });

            var result = await _controller.GetAllProducts();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllProducts_Empty_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllProducts())
                        .ReturnsAsync(new List<ProductDto>());

            var result = await _controller.GetAllProducts();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllProducts_ServiceCalled()
        {
            _serviceMock.Setup(s => s.GetAllProducts())
                        .ReturnsAsync(new List<ProductDto>());

            await _controller.GetAllProducts();

            _serviceMock.Verify(s => s.GetAllProducts(), Times.Once);
        }

        [Test]
        public async Task GetAllProducts_ReturnsCorrectData()
        {
            var data = new List<ProductDto> { CreateProduct() };

            _serviceMock.Setup(s => s.GetAllProducts())
                        .ReturnsAsync(data);

            var result = await _controller.GetAllProducts() as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(data));
        }

        #endregion

        #region GET BY ID

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
                        .ReturnsAsync((ProductDto)null);

            var result = await _controller.GetProductById(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetProductById_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.GetProductById(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetProductById_ServiceCalled()
        {
            _serviceMock.Setup(s => s.GetProductById(1))
                        .ReturnsAsync(CreateProduct());

            await _controller.GetProductById(1);

            _serviceMock.Verify(s => s.GetProductById(1), Times.Once);
        }

        #endregion

        #region CREATE

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

        [Test]
        public async Task CreateProduct_NullRequest_ReturnsOk()
        {
            var result = await _controller.CreateProduct(null);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        #endregion

        #region UPDATE

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
                        .ReturnsAsync((ProductDto)null);

            var result = await _controller.UpdateProduct(1, new UpdateProductRequestDto());

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task UpdateProduct_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.UpdateProduct(0, new UpdateProductRequestDto());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task UpdateProduct_ServiceCalled()
        {
            _serviceMock.Setup(s => s.UpdateProduct(1, It.IsAny<UpdateProductRequestDto>()))
                        .ReturnsAsync(CreateProduct());

            await _controller.UpdateProduct(1, new UpdateProductRequestDto());

            _serviceMock.Verify(s => s.UpdateProduct(1, It.IsAny<UpdateProductRequestDto>()), Times.Once);
        }

        #endregion

        #region DELETE

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

        #endregion
    }
}