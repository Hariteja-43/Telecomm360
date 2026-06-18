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
    public class SubscribersControllerTests
    {
        private Mock<ISubscriberService> _serviceMock;
        private SubscribersController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<ISubscriberService>();
            _controller = new SubscribersController(_serviceMock.Object);
        }

        #region Helpers

        private CreateSubscriberRequestDto CreateCreateRequest()
        {
            return new CreateSubscriberRequestDto
            {
                CustomerId = 1001,
                MSISDN = "9876543210",
                IMSI = "123456789012345",
                DeviceId = "DEVICE123"
            };
        }

        // FIXED (required fields added)
        private UpdateSubscriberRequestDto CreateUpdateRequest()
        {
            return new UpdateSubscriberRequestDto
            {
                CustomerId = 1001,
                MSISDN = "9876543210",
                IMSI = "123456789012345",
                DeviceId = "DEVICE123"
            };
        }

        private SubscriberResponseDto CreateSubscriber(int SubscriberId = 1)
        {
            return new SubscriberResponseDto
            {
                SubscriberId = SubscriberId,
                CustomerId = 1001,
                MSISDN = "9876543210",
                IMSI = "123456789012345",
                DeviceId = "DEVICE123"
            };
        }

        #endregion

        #region CreateSubscriber

        [Test]
        public async Task CreateSubscriber_Valid_ReturnsOk()
        {
            // Arrange
            var request = CreateCreateRequest();
            var response = CreateSubscriber(1);

            _serviceMock.Setup(s => s.CreateSubscriberAsync(request))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateSubscriber(request);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.Not.Null);
        }

        [Test]
        public async Task CreateSubscriber_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "invalid");

            // Act
            var result = await _controller.CreateSubscriber(CreateCreateRequest());

            // Assert
            var badRequest = result as BadRequestObjectResult;
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.Value, Is.EqualTo(GeneralConstants.InvalidInput));
        }

        #endregion

        #region GetAllSubscriber

        [Test]
        public async Task GetAllSubscriber_Valid_ReturnsOk()
        {
            // Arrange
            var list = new List<SubscriberResponseDto>
            {
                CreateSubscriber(1),
                CreateSubscriber(2)
            };

            _serviceMock.Setup(s => s.GetAllSubscribersAsync())
                        .ReturnsAsync(list);

            // Act
            var result = await _controller.GetAllSubscriber();

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.Not.Null);
        }

        #endregion

        #region GetSubscriberById

        [Test]
        public async Task GetSubscriberById_Valid_ReturnsOk()
        {
            // Arrange
            var response = CreateSubscriber(1);

            _serviceMock.Setup(s => s.GetSubscriberByIdAsync(1))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.GetSubscriberById(1);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test]
        public async Task GetSubscriberById_InvalidId_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.GetSubscriberById(0);

            // Assert
            var badRequest = result as BadRequestObjectResult;
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.Value, Is.EqualTo(GeneralConstants.InvalidInput));
        }

        [Test]
        public async Task GetSubscriberById_NotFound_Returns404()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetSubscriberByIdAsync(1))
                        .ReturnsAsync((SubscriberResponseDto)null);

            // Act
            var result = await _controller.GetSubscriberById(1);

            // Assert
            var notFound = result as NotFoundObjectResult;
            Assert.That(notFound, Is.Not.Null);
            Assert.That(notFound.Value, Is.EqualTo(SubscriberConstants.NotFound));
        }

        #endregion

        #region UpdateSubscriberById

        [Test]
        public async Task UpdateSubscriber_Valid_ReturnsOk()
        {
            // Arrange
            var request = CreateUpdateRequest();
            var response = CreateSubscriber(1);

            _serviceMock.Setup(s => s.UpdateSubscriberAsync(1, request))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateSubscriberById(1, request);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test]
        public async Task UpdateSubscriber_InvalidId_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.UpdateSubscriberById(0, CreateUpdateRequest());

            // Assert
            var badRequest = result as BadRequestObjectResult;
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.Value, Is.EqualTo(GeneralConstants.InvalidInput));
        }

        [Test]
        public async Task UpdateSubscriber_NotFound_Returns404()
        {
            // Arrange
            _serviceMock.Setup(s => s.UpdateSubscriberAsync(1, It.IsAny<UpdateSubscriberRequestDto>()))
                        .ReturnsAsync((SubscriberResponseDto)null);

            // Act
            var result = await _controller.UpdateSubscriberById(1, CreateUpdateRequest());

            // Assert
            var notFound = result as NotFoundObjectResult;
            Assert.That(notFound, Is.Not.Null);
            Assert.That(notFound.Value, Is.EqualTo(SubscriberConstants.NotFound));
        }

        #endregion

        #region DeleteSubscriberById

        [Test]
        public async Task DeleteSubscriber_Valid_ReturnsOk()
        {
            // Arrange
            _serviceMock.Setup(s => s.DeleteSubscriberAsync(1))
                        .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteSubscriberById(1);

            // Assert
            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public async Task DeleteSubscriber_InvalidId_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.DeleteSubscriberById(0);

            // Assert
            var badRequest = result as BadRequestObjectResult;
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.Value, Is.EqualTo(GeneralConstants.InvalidInput));
        }

        [Test]
        public async Task DeleteSubscriber_NotFound_Returns404()
        {
            // Arrange
            _serviceMock.Setup(s => s.DeleteSubscriberAsync(1))
                        .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteSubscriberById(1);

            // Assert
            var notFound = result as NotFoundObjectResult;
            Assert.That(notFound, Is.Not.Null);
            Assert.That(notFound.Value, Is.EqualTo(SubscriberConstants.NotFound));
        }

        #endregion
    }
}
