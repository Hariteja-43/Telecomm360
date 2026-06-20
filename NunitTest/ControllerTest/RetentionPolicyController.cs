using NUnit.Framework;
using Moq;
using Telecom360.Controllers;
using Telecom360.Service.Interface;
using Telecom360.DTO.Retention;
using Telecom360.Constant;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Telecom360.Test.ControllerTest
{
    [TestFixture]
    public class RetentionPolicyControllerTests
    {
        private Mock<IRetentionPolicyService> _serviceMock;
        private RetentionPolicyController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IRetentionPolicyService>();
            _controller = new RetentionPolicyController(_serviceMock.Object);
        }

        #region Helpers

        private RetentionPolicyResponseDto CreateTestPolicy(int id = 1)
        {
            return new RetentionPolicyResponseDto
            {
                PolicyID = id,
                DataType = "CustomerData",
                RetentionPeriod = 30,
                AppliedFrom = DateTime.UtcNow
            };
        }

        private CreateRetentionPolicyRequestDto CreateCreateRequest()
        {
            return new CreateRetentionPolicyRequestDto
            {
                DataType = "CustomerData",
                RetentionPeriod = 30
            };
        }

        private UpdateRetentionPolicyRequestDto CreateUpdateRequest()
        {
            return new UpdateRetentionPolicyRequestDto
            {
                DataType = "UpdatedData",
                RetentionPeriod = 60
            };
        }

        #endregion

        #region GetAllRetentionPolicy

        [Test]
        public async Task GetAllRetentionPolicy_Valid_ReturnsOk()
        {
            var policies = new List<RetentionPolicyResponseDto> { CreateTestPolicy() };

            _serviceMock.Setup(s => s.GetAllRetentionPolicy())
                        .ReturnsAsync(policies);

            var result = await _controller.GetAllRetentionPolicy();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllRetentionPolicy_ReturnsCorrectData()
        {
            var policies = new List<RetentionPolicyResponseDto> { CreateTestPolicy() };

            _serviceMock.Setup(s => s.GetAllRetentionPolicy())
                        .ReturnsAsync(policies);

            var result = await _controller.GetAllRetentionPolicy() as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(policies));
        }

        [Test]
        public async Task GetAllRetentionPolicy_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetAllRetentionPolicy())
                        .ReturnsAsync(new List<RetentionPolicyResponseDto>());

            await _controller.GetAllRetentionPolicy();

            _serviceMock.Verify(s => s.GetAllRetentionPolicy(), Times.Once);
        }

        [Test]
        public async Task GetAllRetentionPolicy_Exception_Returns500()
        {
            _serviceMock.Setup(s => s.GetAllRetentionPolicy())
                        .ThrowsAsync(new Exception());

            var result = await _controller.GetAllRetentionPolicy() as ObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(500));
        }

        #endregion

        #region GetRetentionPolicyById

        [Test]
        public async Task GetRetentionPolicyById_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetRetentionPolicyById(1))
                        .ReturnsAsync(CreateTestPolicy());

            var result = await _controller.GetRetentionPolicyById(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetRetentionPolicyById_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.GetRetentionPolicyById(1))
                        .ReturnsAsync((RetentionPolicyResponseDto)null);

            var result = await _controller.GetRetentionPolicyById(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetRetentionPolicyById_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.GetRetentionPolicyById(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetRetentionPolicyById_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetRetentionPolicyById(1))
                        .ReturnsAsync(CreateTestPolicy());

            await _controller.GetRetentionPolicyById(1);

            _serviceMock.Verify(s => s.GetRetentionPolicyById(1), Times.Once);
        }

        [Test]
        public async Task GetRetentionPolicyById_Exception_Returns500()
        {
            _serviceMock.Setup(s => s.GetRetentionPolicyById(It.IsAny<int>()))
                        .ThrowsAsync(new Exception());

            var result = await _controller.GetRetentionPolicyById(1) as ObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(500));
        }

        #endregion

        #region CreateRetentionPolicy

        [Test]
        public async Task CreateRetentionPolicy_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CreateRetentionPolicy(It.IsAny<CreateRetentionPolicyRequestDto>()))
                        .ReturnsAsync(CreateTestPolicy());

            var result = await _controller.CreateRetentionPolicy(CreateCreateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateRetentionPolicy_NullRequest_ReturnsOk()
        {
            // Arrange/Act
            // Controller currently does not null-guard request; it returns Ok(serviceResult).
            _serviceMock.Setup(s => s.CreateRetentionPolicy(null))
                        .ReturnsAsync((RetentionPolicyResponseDto)null);

            var result = await _controller.CreateRetentionPolicy(null);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateRetentionPolicy_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.CreateRetentionPolicy(It.IsAny<CreateRetentionPolicyRequestDto>()))
                        .ReturnsAsync(CreateTestPolicy());

            await _controller.CreateRetentionPolicy(CreateCreateRequest());

            _serviceMock.Verify(s => s.CreateRetentionPolicy(It.IsAny<CreateRetentionPolicyRequestDto>()), Times.Once);
        }

        [Test]
        public async Task CreateRetentionPolicy_Exception_Returns500()
        {
            _serviceMock.Setup(s => s.CreateRetentionPolicy(It.IsAny<CreateRetentionPolicyRequestDto>()))
                        .ThrowsAsync(new Exception());

            var result = await _controller.CreateRetentionPolicy(CreateCreateRequest()) as ObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(500));
        }

        #endregion

        #region UpdateRetentionPolicy

        [Test]
        public async Task UpdateRetentionPolicy_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.UpdateRetentionPolicy(1, It.IsAny<UpdateRetentionPolicyRequestDto>()))
                        .ReturnsAsync(CreateTestPolicy());

            var result = await _controller.UpdateRetentionPolicy(1, CreateUpdateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task UpdateRetentionPolicy_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.UpdateRetentionPolicy(1, It.IsAny<UpdateRetentionPolicyRequestDto>()))
                        .ReturnsAsync((RetentionPolicyResponseDto)null);

            var result = await _controller.UpdateRetentionPolicy(1, CreateUpdateRequest());

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task UpdateRetentionPolicy_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.UpdateRetentionPolicy(0, CreateUpdateRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task UpdateRetentionPolicy_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.UpdateRetentionPolicy(1, It.IsAny<UpdateRetentionPolicyRequestDto>()))
                        .ReturnsAsync(CreateTestPolicy());

            await _controller.UpdateRetentionPolicy(1, CreateUpdateRequest());

            _serviceMock.Verify(s => s.UpdateRetentionPolicy(1, It.IsAny<UpdateRetentionPolicyRequestDto>()), Times.Once);
        }

        [Test]
        public async Task UpdateRetentionPolicy_Exception_Returns500()
        {
            _serviceMock.Setup(s => s.UpdateRetentionPolicy(It.IsAny<int>(), It.IsAny<UpdateRetentionPolicyRequestDto>()))
                        .ThrowsAsync(new Exception());

            var result = await _controller.UpdateRetentionPolicy(1, CreateUpdateRequest()) as ObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(500));
        }

        #endregion
    }
}