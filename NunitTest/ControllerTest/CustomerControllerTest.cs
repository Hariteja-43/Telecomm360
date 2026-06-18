using NUnit.Framework;
using Moq;
using Telecomm360.Controllers;
using Telecomm360.Service.Interface;
using Telecomm360.DTO;
using Telecomm360.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private Mock<ICustomerService> _serviceMock;
        private CustomerController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<ICustomerService>();
            _controller = new CustomerController(_serviceMock.Object);
        }

        // helpers

        private CreateCustomerRequestDto CreateRequest()
        {
            return new CreateCustomerRequestDto
            {
                Name = "Test Customer",
                Type = "Individual",
                KYCStatus = "Verified",
                ContactInfo = "9999999999"
            };
        }

        private UpdateCustomerRequestDto UpdateRequest()
        {
            return new UpdateCustomerRequestDto
            {
                Name = "Updated Name",
                Type = "Corporate",
                KYCStatus = "Verified",
                ContactInfo = "8888888888"
            };
        }

        private CustomerResponseDto CreateResponse(int CustomerId = 1)
        {
            return new CustomerResponseDto
            {
                CustomerId = CustomerId,
                Name = "Test Customer",
                Type = "Individual",
                KYCStatus = "Verified",
                ContactInfo = "9999999999"
            };
        }

        #region CreateCustomer

        [Test]
        public async Task CreateCustomer_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CreateCustomerAsync(It.IsAny<CreateCustomerRequestDto>()))
                        .ReturnsAsync(CreateResponse());

            var result = await _controller.CreateCustomer(CreateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateCustomer_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("x", "error");

            var result = await _controller.CreateCustomer(CreateRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task CreateCustomer_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.CreateCustomerAsync(It.IsAny<CreateCustomerRequestDto>()))
                        .ReturnsAsync(CreateResponse());

            await _controller.CreateCustomer(CreateRequest());

            _serviceMock.Verify(s => s.CreateCustomerAsync(It.IsAny<CreateCustomerRequestDto>()), Times.Once);
        }

        #endregion

        #region GetAllCustomers

        [Test]
        public async Task GetAllCustomers_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllCustomersAsync())
                        .ReturnsAsync(new List<CustomerResponseDto> { CreateResponse() });

            var result = await _controller.GetAllCustomers();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllCustomers_Empty_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllCustomersAsync())
                        .ReturnsAsync(new List<CustomerResponseDto>());

            var result = await _controller.GetAllCustomers();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllCustomers_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetAllCustomersAsync())
                        .ReturnsAsync(new List<CustomerResponseDto>());

            await _controller.GetAllCustomers();

            _serviceMock.Verify(s => s.GetAllCustomersAsync(), Times.Once);
        }

        #endregion

        #region GetCustomerById

        [Test]
        public async Task GetCustomerById_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetCustomerByIdAsync(1))
                        .ReturnsAsync(CreateResponse());

            var result = await _controller.GetCustomerById(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetCustomerById_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.GetCustomerById(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetCustomerById_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.GetCustomerByIdAsync(1))
                        .ReturnsAsync((CustomerResponseDto?)null);

            var result = await _controller.GetCustomerById(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        #endregion

        #region UpdateCustomer

        [Test]
        public async Task UpdateCustomer_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.UpdateCustomerAsync(1, It.IsAny<UpdateCustomerRequestDto>()))
                        .ReturnsAsync(CreateResponse());

            var result = await _controller.UpdateCustomer(1, UpdateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task UpdateCustomer_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.UpdateCustomer(0, UpdateRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task UpdateCustomer_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.UpdateCustomerAsync(1, It.IsAny<UpdateCustomerRequestDto>()))
                        .ReturnsAsync((CustomerResponseDto?)null);

            var result = await _controller.UpdateCustomer(1, UpdateRequest());

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        #endregion

        #region DeleteCustomer

        [Test]
        public async Task DeleteCustomer_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.DeleteCustomerAsync(1))
                        .ReturnsAsync(true);

            var result = await _controller.DeleteCustomer(1);

            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public async Task DeleteCustomer_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.DeleteCustomer(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task DeleteCustomer_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.DeleteCustomerAsync(1))
                        .ReturnsAsync(false);

            var result = await _controller.DeleteCustomer(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        #endregion
    }
}