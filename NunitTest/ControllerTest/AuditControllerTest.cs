using NUnit.Framework;
using Moq;
using Telecomm360.Controllers;
using Telecomm360.Service.Interface;
using Telecomm360.DTO;
using Telecomm360.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class AuditControllerTests
    {
        private Mock<IAuditLogService> _serviceMock;
        private AuditController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IAuditLogService>();
            _controller = new AuditController(_serviceMock.Object);
        }

        #region Helpers

        private AuditLogSearchDto CreateSearch() => new AuditLogSearchDto();

        private AuditLogCreateRequest CreateRequest()
        {
            return new AuditLogCreateRequest
            {
                UserId = 1,
                ActionPerformed = "Create",
                TargetResource = "Subscriber"
            };
        }

        private AuditLogResponse CreateResponse()
        {
            return new AuditLogResponse
            {
                AuditLogID = 1,
                UserID = 1,
                Action = "Create",
                Resource = "Subscriber",
                Timestamp = DateTime.UtcNow
            };
        }

        #endregion

        #region GetAuditLogs

        [Test]
        public async Task GetAuditLogs_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAuditLogsAsync(It.IsAny<AuditLogSearchDto>()))
                        .ReturnsAsync(new List<AuditLogResponse> { CreateResponse() });

            var result = await _controller.GetAuditLogs(CreateSearch());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAuditLogs_Valid_ReturnsCorrectData()
        {
            var data = new List<AuditLogResponse> { CreateResponse() };

            _serviceMock.Setup(s => s.GetAuditLogsAsync(It.IsAny<AuditLogSearchDto>()))
                        .ReturnsAsync(data);

            var result = await _controller.GetAuditLogs(CreateSearch()) as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(data));
        }

        [Test]
        public async Task GetAuditLogs_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.GetAuditLogs(CreateSearch());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetAuditLogs_EmptyList_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAuditLogsAsync(It.IsAny<AuditLogSearchDto>()))
                        .ReturnsAsync(new List<AuditLogResponse>());

            var result = await _controller.GetAuditLogs(CreateSearch());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAuditLogs_ServiceReturnsNull_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAuditLogsAsync(It.IsAny<AuditLogSearchDto>()))
                        .ReturnsAsync((List<AuditLogResponse>)null);

            var result = await _controller.GetAuditLogs(CreateSearch());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAuditLogs_ServiceCalled_Once()
        {
            _serviceMock.Setup(s => s.GetAuditLogsAsync(It.IsAny<AuditLogSearchDto>()))
                        .ReturnsAsync(new List<AuditLogResponse>());

            await _controller.GetAuditLogs(CreateSearch());

            _serviceMock.Verify(s => s.GetAuditLogsAsync(It.IsAny<AuditLogSearchDto>()), Times.Once);
        }

        #endregion

        #region CreateAuditLog

        [Test]
        public async Task CreateAuditLog_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CreateAuditLogAsync(It.IsAny<AuditLogCreateRequest>()))
                        .Returns(Task.CompletedTask);

            var result = await _controller.CreateAuditLog(CreateRequest());

            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public async Task CreateAuditLog_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.CreateAuditLog(CreateRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task CreateAuditLog_ServiceCalled_Once()
        {
            _serviceMock.Setup(s => s.CreateAuditLogAsync(It.IsAny<AuditLogCreateRequest>()))
                        .Returns(Task.CompletedTask);

            await _controller.CreateAuditLog(CreateRequest());

            _serviceMock.Verify(s => s.CreateAuditLogAsync(It.IsAny<AuditLogCreateRequest>()), Times.Once);
        }

        [Test]
        public async Task CreateAuditLog_NullRequest_ReturnsOk()
        {
            // Controller does not null-guard request; it returns Ok when ModelState is valid.
            var result = await _controller.CreateAuditLog(null);

            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public async Task CreateAuditLog_ServiceThrowsException()
        {
            _serviceMock.Setup(s => s.CreateAuditLogAsync(It.IsAny<AuditLogCreateRequest>()))
                        .ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(async () => await _controller.CreateAuditLog(CreateRequest()));
        }

        #endregion

        #region AdditionalCoverage

        [Test]
        public async Task GetAuditLogs_NullSearch_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAuditLogsAsync(It.IsAny<AuditLogSearchDto>()))
                        .ReturnsAsync(new List<AuditLogResponse>());

            var result = await _controller.GetAuditLogs(null);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAuditLogs_ResponseNotNull()
        {
            _serviceMock.Setup(s => s.GetAuditLogsAsync(It.IsAny<AuditLogSearchDto>()))
                        .ReturnsAsync(new List<AuditLogResponse> { CreateResponse() });

            var result = await _controller.GetAuditLogs(CreateSearch()) as OkObjectResult;

            Assert.That(result.Value, Is.Not.Null);
        }

        [Test]
        public async Task CreateAuditLog_ResponseTypeCheck()
        {
            _serviceMock.Setup(s => s.CreateAuditLogAsync(It.IsAny<AuditLogCreateRequest>()))
                        .Returns(Task.CompletedTask);

            var result = await _controller.CreateAuditLog(CreateRequest());

            Assert.That(result, Is.InstanceOf<OkResult>());
        }

        #endregion
    }
}