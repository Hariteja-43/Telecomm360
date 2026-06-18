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

namespace Telecom360.Test.ControllerTest
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

        #region ✅ Helpers

        // ✅ FIXED: all required fields initialized
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

        #region ✅ GetAllComplianceReports

        [Test]
        public async Task GetAllComplianceReports_Valid_ReturnsOk()
        {
            // Arrange
            var reports = new List<ComplianceReportResponseDto>
            {
                CreateTestReport(1),
                CreateTestReport(2)
            };

            _serviceMock.Setup(s => s.GetAllComplianceReports())
                        .ReturnsAsync(reports);

            // Act
            var result = await _controller.GetAllComplianceReports();

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(reports));
        }

        #endregion

        #region ✅ GetComplianceReportById

        [Test]
        public async Task GetComplianceReportById_Valid_ReturnsOk()
        {
            // Arrange
            var report = CreateTestReport(1);

            _serviceMock.Setup(s => s.GetComplianceReportById(1))
                        .ReturnsAsync(report);

            // Act
            var result = await _controller.GetComplianceReportById(1);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(report));
        }

        [Test]
        public async Task GetComplianceReportById_NotFound_Returns404()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetComplianceReportById(1))
                        .ReturnsAsync((ComplianceReportResponseDto)null);

            // Act
            var result = await _controller.GetComplianceReportById(1);

            // Assert
            var notFound = result as NotFoundObjectResult;
            Assert.That(notFound, Is.Not.Null);
            Assert.That(notFound.Value, Is.EqualTo(ErrorMessages.NOT_FOUND));
        }

        [Test]
        public async Task GetComplianceReportById_Exception_Returns500()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetComplianceReportById(It.IsAny<int>()))
                        .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetComplianceReportById(1);

            // Assert
            var status = result as ObjectResult;
            Assert.That(status, Is.Not.Null);
            Assert.That(status.StatusCode, Is.EqualTo(500));
            Assert.That(status.Value, Is.EqualTo(ErrorMessages.SERVER_ERROR));
        }

        #endregion

        #region ✅ CreateComplianceReport

        [Test]
        public async Task CreateComplianceReport_Valid_ReturnsOk()
        {
            // Arrange
            var request = CreateRequest();
            var response = CreateTestReport(1);

            _serviceMock.Setup(s => s.CreateComplianceReport(request))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateComplianceReport(request);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task CreateComplianceReport_Exception_Returns500()
        {
            // Arrange
            _serviceMock.Setup(s => s.CreateComplianceReport(It.IsAny<GenerateComplianceReportRequestDto>()))
                        .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.CreateComplianceReport(CreateRequest());

            // Assert
            var status = result as ObjectResult;
            Assert.That(status, Is.Not.Null);
            Assert.That(status.StatusCode, Is.EqualTo(500));
        }

        #endregion
    }
}