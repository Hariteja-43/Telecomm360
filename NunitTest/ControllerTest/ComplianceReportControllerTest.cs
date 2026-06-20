using NUnit.Framework;
using Moq;
using Telecom360.Controllers;
using Telecom360.Service.Interface;
using Telecom360.DTO.Compliance;
using Telecom360.Constant;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class ComplianceReportControllerTests
    {
        private Mock<IComplianceReportService> _serviceMock;
        private ComplianceReportController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IComplianceReportService>();
            _controller = new ComplianceReportController(_serviceMock.Object);
        }

        #region Helpers

        private ComplianceReportResponseDto CreateTestReport(int id = 1)
        {
            return new ComplianceReportResponseDto
            {
                ComplianceReportId = id,
                Type = "Monthly",
                Scope = "Global",
                GeneratedDate = DateTime.UtcNow
            };
        }

        private GenerateComplianceReportRequestDto CreateRequest()
        {
            return new GenerateComplianceReportRequestDto
            {
                Type = "Monthly",
                Scope = "Global"
            };
        }

        #endregion

        #region GetAllComplianceReports

        [Test]
        public async Task GetAllComplianceReports_Valid_ReturnsOk()
        {
            var reports = new List<ComplianceReportResponseDto> { CreateTestReport() };

            _serviceMock.Setup(s => s.GetAllComplianceReports())
                        .ReturnsAsync(reports);

            var result = await _controller.GetAllComplianceReports();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllComplianceReports_ReturnsCorrectData()
        {
            var reports = new List<ComplianceReportResponseDto> { CreateTestReport() };

            _serviceMock.Setup(s => s.GetAllComplianceReports())
                        .ReturnsAsync(reports);

            var result = await _controller.GetAllComplianceReports() as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(reports));
        }

        [Test]
        public async Task GetAllComplianceReports_EmptyList_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllComplianceReports())
                        .ReturnsAsync(new List<ComplianceReportResponseDto>());

            var result = await _controller.GetAllComplianceReports();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllComplianceReports_ServiceCalled_Once()
        {
            _serviceMock.Setup(s => s.GetAllComplianceReports())
                        .ReturnsAsync(new List<ComplianceReportResponseDto>());

            await _controller.GetAllComplianceReports();

            _serviceMock.Verify(s => s.GetAllComplianceReports(), Times.Once);
        }

        #endregion

        #region GetComplianceReportById

        [Test]
        public async Task GetComplianceReportById_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetComplianceReportById(1))
                        .ReturnsAsync(CreateTestReport());

            var result = await _controller.GetComplianceReportById(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetComplianceReportById_ReturnsCorrectData()
        {
            var report = CreateTestReport();

            _serviceMock.Setup(s => s.GetComplianceReportById(1))
                        .ReturnsAsync(report);

            var result = await _controller.GetComplianceReportById(1) as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(report));
        }

        [Test]
        public async Task GetComplianceReportById_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.GetComplianceReportById(1))
                        .ReturnsAsync((ComplianceReportResponseDto)null);

            var result = await _controller.GetComplianceReportById(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetComplianceReportById_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.GetComplianceReportById(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetComplianceReportById_ServiceCalled_Once()
        {
            _serviceMock.Setup(s => s.GetComplianceReportById(1))
                        .ReturnsAsync(CreateTestReport());

            await _controller.GetComplianceReportById(1);

            _serviceMock.Verify(s => s.GetComplianceReportById(1), Times.Once);
        }

        [Test]
        public async Task GetComplianceReportById_Exception_Returns500()
        {
            _serviceMock.Setup(s => s.GetComplianceReportById(It.IsAny<int>()))
                        .ThrowsAsync(new Exception());

            var result = await _controller.GetComplianceReportById(1) as ObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(500));
        }

        #endregion

        #region CreateComplianceReport

        [Test]
        public async Task CreateComplianceReport_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CreateComplianceReport(It.IsAny<GenerateComplianceReportRequestDto>()))
                        .ReturnsAsync(CreateTestReport());

            var result = await _controller.CreateComplianceReport(CreateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateComplianceReport_ReturnsCorrectData()
        {
            var response = CreateTestReport();

            _serviceMock.Setup(s => s.CreateComplianceReport(It.IsAny<GenerateComplianceReportRequestDto>()))
                        .ReturnsAsync(response);

            var result = await _controller.CreateComplianceReport(CreateRequest()) as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task CreateComplianceReport_NullRequest_ReturnsOk()
        {
            var result = await _controller.CreateComplianceReport(null);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateComplianceReport_ServiceCalled_Once()
        {
            _serviceMock.Setup(s => s.CreateComplianceReport(It.IsAny<GenerateComplianceReportRequestDto>()))
                        .ReturnsAsync(CreateTestReport());

            await _controller.CreateComplianceReport(CreateRequest());

            _serviceMock.Verify(s => s.CreateComplianceReport(It.IsAny<GenerateComplianceReportRequestDto>()), Times.Once);
        }

        [Test]
        public async Task CreateComplianceReport_ServiceReturnsNull_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CreateComplianceReport(It.IsAny<GenerateComplianceReportRequestDto>()))
                        .ReturnsAsync((ComplianceReportResponseDto)null);

            var result = await _controller.CreateComplianceReport(CreateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateComplianceReport_Exception_Returns500()
        {
            _serviceMock.Setup(s => s.CreateComplianceReport(It.IsAny<GenerateComplianceReportRequestDto>()))
                        .ThrowsAsync(new Exception());

            var result = await _controller.CreateComplianceReport(CreateRequest()) as ObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(500));
        }

        #endregion
    }
}