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

        // FIXED: required DataType added
        private RetentionPolicyResponseDto CreateTestPolicy(int id = 1)
        {
            return new RetentionPolicyResponseDto
            {
                PolicyID = id,
                DataType = "CustomerData",     // REQUIRED
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
            // Arrange
            var policies = new List<RetentionPolicyResponseDto>
            {
                CreateTestPolicy(1),
                CreateTestPolicy(2)
            };

            _serviceMock.Setup(s => s.GetAllRetentionPolicy())
                        .ReturnsAsync(policies);

            // Act
            var result = await _controller.GetAllRetentionPolicy();

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(policies));
        }

        [Test]
        public async Task GetAllRetentionPolicy_Exception_Returns500()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetAllRetentionPolicy())
                        .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetAllRetentionPolicy();

            // Assert
            var status = result as ObjectResult;
            Assert.That(status, Is.Not.Null);
            Assert.That(status.StatusCode, Is.EqualTo(500));
            Assert.That(status.Value, Is.EqualTo(ErrorMessages.SERVER_ERROR));
        }

        #endregion

        #region GetRetentionPolicyById

        [Test]
        public async Task GetRetentionPolicyById_Valid_ReturnsOk()
        {
            // Arrange
            var policy = CreateTestPolicy(1);

            _serviceMock.Setup(s => s.GetRetentionPolicyById(1))
                        .ReturnsAsync(policy);

            // Act
            var result = await _controller.GetRetentionPolicyById(1);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(policy));
        }

        [Test]
        public async Task GetRetentionPolicyById_NotFound_Returns404()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetRetentionPolicyById(1))
                        .ReturnsAsync((RetentionPolicyResponseDto)null);

            // Act
            var result = await _controller.GetRetentionPolicyById(1);

            // Assert
            var notFound = result as NotFoundObjectResult;
            Assert.That(notFound, Is.Not.Null);
            Assert.That(notFound.Value, Is.EqualTo(ErrorMessages.NOT_FOUND));
        }

        [Test]
        public async Task GetRetentionPolicyById_Exception_Returns500()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetRetentionPolicyById(It.IsAny<int>()))
                        .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetRetentionPolicyById(1);

            // Assert
            var status = result as ObjectResult;
            Assert.That(status, Is.Not.Null);
            Assert.That(status.StatusCode, Is.EqualTo(500));
        }

        #endregion

        #region CreateRetentionPolicy

        [Test]
        public async Task CreateRetentionPolicy_Valid_ReturnsOk()
        {
            // Arrange
            var request = CreateCreateRequest();
            var response = CreateTestPolicy(1);

            _serviceMock.Setup(s => s.CreateRetentionPolicy(request))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateRetentionPolicy(request);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task CreateRetentionPolicy_Exception_Returns500()
        {
            // Arrange
            _serviceMock.Setup(s => s.CreateRetentionPolicy(It.IsAny<CreateRetentionPolicyRequestDto>()))
                        .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.CreateRetentionPolicy(CreateCreateRequest());

            // Assert
            var status = result as ObjectResult;
            Assert.That(status, Is.Not.Null);
            Assert.That(status.StatusCode, Is.EqualTo(500));
        }

        #endregion

        #region UpdateRetentionPolicy

        [Test]
        public async Task UpdateRetentionPolicy_Valid_ReturnsOk()
        {
            // Arrange
            var request = CreateUpdateRequest();
            var response = CreateTestPolicy(1);

            _serviceMock.Setup(s => s.UpdateRetentionPolicy(1, request))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateRetentionPolicy(1, request);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task UpdateRetentionPolicy_NotFound_Returns404()
        {
            // Arrange
            var request = CreateUpdateRequest();

            _serviceMock.Setup(s => s.UpdateRetentionPolicy(1, request))
                        .ReturnsAsync((RetentionPolicyResponseDto)null);

            // Act
            var result = await _controller.UpdateRetentionPolicy(1, request);

            // Assert
            var notFound = result as NotFoundObjectResult;
            Assert.That(notFound, Is.Not.Null);
            Assert.That(notFound.Value, Is.EqualTo(ErrorMessages.NOT_FOUND));
        }

        [Test]
        public async Task UpdateRetentionPolicy_Exception_Returns500()
        {
            // Arrange
            _serviceMock.Setup(s => s.UpdateRetentionPolicy(It.IsAny<int>(), It.IsAny<UpdateRetentionPolicyRequestDto>()))
                        .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.UpdateRetentionPolicy(1, CreateUpdateRequest());

            // Assert
            var status = result as ObjectResult;
            Assert.That(status, Is.Not.Null);
            Assert.That(status.StatusCode, Is.EqualTo(500));
        }

        #endregion
    }
}