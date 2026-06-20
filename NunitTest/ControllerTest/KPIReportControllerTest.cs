using NUnit.Framework;
using Moq;
using Telecomm360.Controllers;
using Telecomm360.Service.Interfaces;
using Telecomm360.DTOs;
using Telecomm360.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class KPIReportControllerTests
    {
        private Mock<IKPIReportService> _serviceMock;
        private KPIReportController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IKPIReportService>();
            _controller = new KPIReportController(_serviceMock.Object);
        }

        #region Helpers

        private SearchDto CreateSearch() => new SearchDto();

        private KPIReportDto CreateDto(int KPIReportId = 1)
        {
            return new KPIReportDto
            {
                KPIReportId = KPIReportId,
                GeneratedDate = DateTime.UtcNow,
                Metrics = "Revenue, Growth",
                Scope = "GLOBAL"
            };
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllKPIReport(It.IsAny<SearchDto>()))
                        .Returns(new List<KPIReportDto> { CreateDto() });

            var result = _controller.GetAll(CreateSearch());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetAll_Empty_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllKPIReport(It.IsAny<SearchDto>()))
                        .Returns(new List<KPIReportDto>());

            var result = _controller.GetAll(CreateSearch());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetAll_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetAllKPIReport(It.IsAny<SearchDto>()))
                        .Returns(new List<KPIReportDto>());

            _controller.GetAll(CreateSearch());

            _serviceMock.Verify(s => s.GetAllKPIReport(It.IsAny<SearchDto>()), Times.Once);
        }

        [Test]
        public void GetAll_ReturnsCorrectData()
        {
            var data = new List<KPIReportDto> { CreateDto() };

            _serviceMock.Setup(s => s.GetAllKPIReport(It.IsAny<SearchDto>()))
                        .Returns(data);

            var result = _controller.GetAll(CreateSearch()) as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(data));
        }

        #endregion

        #region GetKPIReportById

        [Test]
        public void GetById_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetKPIReportById(1))
                        .Returns(CreateDto());

            var result = _controller.GetKPIReportById(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetById_InvalidId_ReturnsBadRequest()
        {
            var result = _controller.GetKPIReportById(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void GetById_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.GetKPIReportById(1))
                        .Returns((KPIReportDto)null);

            var result = _controller.GetKPIReportById(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public void GetById_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetKPIReportById(1))
                        .Returns(CreateDto());

            _controller.GetKPIReportById(1);

            _serviceMock.Verify(s => s.GetKPIReportById(1), Times.Once);
        }

        #endregion

        #region GetKPIReportByScope

        [Test]
        public void GetByScope_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetKPIReportByScope(It.IsAny<string>()))
                        .Returns(new List<KPIReportDto> { CreateDto() });

            var result = _controller.GetKPIReportByScope("GLOBAL");

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetByScope_InvalidScope_ReturnsBadRequest()
        {
            var result = _controller.GetKPIReportByScope("");

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void GetByScope_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetKPIReportByScope(It.IsAny<string>()))
                        .Returns(new List<KPIReportDto>());

            _controller.GetKPIReportByScope("GLOBAL");

            _serviceMock.Verify(s => s.GetKPIReportByScope(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetByScope_ReturnsCorrectData()
        {
            var data = new List<KPIReportDto> { CreateDto() };

            _serviceMock.Setup(s => s.GetKPIReportByScope(It.IsAny<string>()))
                        .Returns(data);

            var result = _controller.GetKPIReportByScope("GLOBAL") as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(data));
        }

        #endregion

        #region Create

        [Test]
        public void Create_Valid_ReturnsCreated()
        {
            _serviceMock.Setup(s => s.CreateKPIReport(It.IsAny<KPIReportDto>()))
                        .Returns(CreateDto());

            var result = _controller.Create(CreateDto());

            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
        }

        [Test]
        public void Create_Null_ReturnsBadRequest()
        {
            var result = _controller.Create(null);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Create_ActionNameCorrect()
        {
            _serviceMock.Setup(s => s.CreateKPIReport(It.IsAny<KPIReportDto>()))
                        .Returns(CreateDto());

            var result = _controller.Create(CreateDto()) as CreatedAtActionResult;

            Assert.That(result.ActionName, Is.EqualTo("GetKPIReportById"));
        }

        [Test]
        public void Create_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.CreateKPIReport(It.IsAny<KPIReportDto>()))
                        .Returns(CreateDto());

            _controller.Create(CreateDto());

            _serviceMock.Verify(s => s.CreateKPIReport(It.IsAny<KPIReportDto>()), Times.Once);
        }

        #endregion

        #region Delete

        [Test]
        public void Delete_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetKPIReportById(1))
                        .Returns(CreateDto());

            var result = _controller.Delete(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void Delete_InvalidId_ReturnsBadRequest()
        {
            var result = _controller.Delete(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Delete_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.GetKPIReportById(1))
                        .Returns((KPIReportDto)null);

            var result = _controller.Delete(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        #endregion

        #region Export

        [Test]
        public void Export_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetKPIReportById(1))
                        .Returns(CreateDto());

            var result = _controller.Export(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void Export_InvalidId_ReturnsBadRequest()
        {
            var result = _controller.Export(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Export_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.GetKPIReportById(1))
                        .Returns((KPIReportDto)null);

            var result = _controller.Export(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        #endregion
    }
}