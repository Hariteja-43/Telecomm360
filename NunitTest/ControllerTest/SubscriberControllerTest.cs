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

        private SubscriberResponseDto CreateSubscriber(int id = 1)
        {
            return new SubscriberResponseDto
            {
                SubscriberId = id,
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
            _serviceMock.Setup(s => s.CreateSubscriberAsync(It.IsAny<CreateSubscriberRequestDto>()))
                        .ReturnsAsync(CreateSubscriber());

            var result = await _controller.CreateSubscriber(CreateCreateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateSubscriber_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.CreateSubscriber(CreateCreateRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task CreateSubscriber_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.CreateSubscriberAsync(It.IsAny<CreateSubscriberRequestDto>()))
                        .ReturnsAsync(CreateSubscriber());

            await _controller.CreateSubscriber(CreateCreateRequest());

            _serviceMock.Verify(s => s.CreateSubscriberAsync(It.IsAny<CreateSubscriberRequestDto>()), Times.Once);
        }

        [Test]
        public async Task CreateSubscriber_NullRequest_ReturnsOk()
        {
            var result = await _controller.CreateSubscriber(null);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        #endregion

        #region GetAllSubscriber

        [Test]
        public async Task GetAllSubscriber_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllSubscribersAsync())
                        .ReturnsAsync(new List<SubscriberResponseDto> { CreateSubscriber() });

            var result = await _controller.GetAllSubscriber();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllSubscriber_Empty_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllSubscribersAsync())
                        .ReturnsAsync(new List<SubscriberResponseDto>());

            var result = await _controller.GetAllSubscriber();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllSubscriber_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetAllSubscribersAsync())
                        .ReturnsAsync(new List<SubscriberResponseDto>());

            await _controller.GetAllSubscriber();

            _serviceMock.Verify(s => s.GetAllSubscribersAsync(), Times.Once);
        }

        [Test]
        public async Task GetAllSubscriber_ReturnsCorrectData()
        {
            var data = new List<SubscriberResponseDto> { CreateSubscriber() };

            _serviceMock.Setup(s => s.GetAllSubscribersAsync())
                        .ReturnsAsync(data);

            var result = await _controller.GetAllSubscriber() as OkObjectResult;

            var wrapped = result.Value as dynamic;
            Assert.That(wrapped.data, Is.EqualTo(data));
        }

        #endregion

        #region GetSubscriberById

        [Test]
        public async Task GetSubscriberById_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetSubscriberByIdAsync(1))
                        .ReturnsAsync(CreateSubscriber());

            var result = await _controller.GetSubscriberById(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetSubscriberById_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.GetSubscriberById(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetSubscriberById_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.GetSubscriberByIdAsync(1))
                        .ReturnsAsync((SubscriberResponseDto)null);

            var result = await _controller.GetSubscriberById(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetSubscriberById_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetSubscriberByIdAsync(1))
                        .ReturnsAsync(CreateSubscriber());

            await _controller.GetSubscriberById(1);

            _serviceMock.Verify(s => s.GetSubscriberByIdAsync(1), Times.Once);
        }

        #endregion

        #region UpdateSubscriberById

        [Test]
        public async Task UpdateSubscriber_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.UpdateSubscriberAsync(1, It.IsAny<UpdateSubscriberRequestDto>()))
                        .ReturnsAsync(CreateSubscriber());

            var result = await _controller.UpdateSubscriberById(1, CreateUpdateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task UpdateSubscriber_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.UpdateSubscriberById(0, CreateUpdateRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task UpdateSubscriber_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.UpdateSubscriberAsync(1, It.IsAny<UpdateSubscriberRequestDto>()))
                        .ReturnsAsync((SubscriberResponseDto)null);

            var result = await _controller.UpdateSubscriberById(1, CreateUpdateRequest());

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task UpdateSubscriber_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.UpdateSubscriberAsync(1, It.IsAny<UpdateSubscriberRequestDto>()))
                        .ReturnsAsync(CreateSubscriber());

            await _controller.UpdateSubscriberById(1, CreateUpdateRequest());

            _serviceMock.Verify(s => s.UpdateSubscriberAsync(1, It.IsAny<UpdateSubscriberRequestDto>()), Times.Once);
        }

        #endregion

        #region DeleteSubscriberById

        [Test]
        public async Task DeleteSubscriber_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.DeleteSubscriberAsync(1))
                        .ReturnsAsync(true);

            var result = await _controller.DeleteSubscriberById(1);

            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public async Task DeleteSubscriber_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.DeleteSubscriberById(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task DeleteSubscriber_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.DeleteSubscriberAsync(1))
                        .ReturnsAsync(false);

            var result = await _controller.DeleteSubscriberById(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        #endregion
    }
}