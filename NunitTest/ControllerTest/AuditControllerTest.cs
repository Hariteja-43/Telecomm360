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

        // helpers

        private AuditLogSearchDto CreateSearch()
        {
            return new AuditLogSearchDto();
        }

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

        #region GetAuditLogs

        [Test]
        public async Task GetAuditLogs_Valid_ReturnsOk()
        {
            var list = new List<AuditLogResponse> { CreateResponse() };

            _serviceMock.Setup(s => s.GetAuditLogsAsync(It.IsAny<AuditLogSearchDto>()))
                        .ReturnsAsync(list);

            var result = await _controller.GetAuditLogs(CreateSearch());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
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
        public async Task CreateAuditLog_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.CreateAuditLogAsync(It.IsAny<AuditLogCreateRequest>()))
                        .Returns(Task.CompletedTask);

            await _controller.CreateAuditLog(CreateRequest());

            _serviceMock.Verify(s => s.CreateAuditLogAsync(It.IsAny<AuditLogCreateRequest>()), Times.Once);
        }

        #endregion
    }
}
