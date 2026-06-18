using NUnit.Framework;
using Moq;
using Telecomm360.Controllers;
using Telecomm360.DTOs;
using Telecomm360.DTO;
using Telecomm360.Constants;
using Telecomm360.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class AnalyticsDatasetControllerTests
    {
        private Mock<IAnalyticsService> _serviceMock;
        private AnalyticsDatasetController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IAnalyticsService>();
            _controller = new AnalyticsDatasetController(_serviceMock.Object);
        }

        // Helpers
        private SearchDto CreateSearch()
        {
            return new SearchDto();
        }

        private AnalyticsDatasetDto CreateDto()
        {
            return new AnalyticsDatasetDto
            {
                DatasetID = 1,
                Name = "Dataset-1",
                Schema = "Schema-1",
                LastRefreshed = DateTime.UtcNow
            };
        }

        #region GetAll

        [Test]
        public void GetAll_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllAnalyticsDatasets(It.IsAny<SearchDto>()))
                        .Returns(new List<AnalyticsDatasetDto> { CreateDto() });

            var result = _controller.GetAll(CreateSearch());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetAll_Empty_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllAnalyticsDatasets(It.IsAny<SearchDto>()))
                        .Returns(new List<AnalyticsDatasetDto>());

            var result = _controller.GetAll(CreateSearch());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetAll_ServiceCalled()
        {
            _serviceMock.Setup(s => s.GetAllAnalyticsDatasets(It.IsAny<SearchDto>()))
                        .Returns(new List<AnalyticsDatasetDto>());

            _controller.GetAll(CreateSearch());

            _serviceMock.Verify(s => s.GetAllAnalyticsDatasets(It.IsAny<SearchDto>()), Times.Once);
        }

        #endregion

        #region Create

        [Test]
        public void Create_Valid_ReturnsCreated()
        {
            _serviceMock.Setup(s => s.CreateAnalyticsDataset(It.IsAny<AnalyticsDatasetDto>()))
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
        public void Create_ActionNameCheck()
        {
            _serviceMock.Setup(s => s.CreateAnalyticsDataset(It.IsAny<AnalyticsDatasetDto>()))
                        .Returns(CreateDto());

            var result = _controller.Create(CreateDto()) as CreatedAtActionResult;

            Assert.That(result.ActionName, Is.EqualTo("Get"));
        }

        #endregion

        #region Get

        [Test]
        public void Get_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAnalyticsDataset(1))
                        .Returns(CreateDto());

            var result = _controller.Get(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void Get_InvalidId_ReturnsBadRequest()
        {
            var result = _controller.Get(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Get_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.GetAnalyticsDataset(1))
                        .Returns((AnalyticsDatasetDto)null);

            var result = _controller.Get(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        #endregion

        #region Refresh

        [Test]
        public void Refresh_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.RefreshAnalyticsDataset(1))
                        .Returns(CreateDto());

            var result = _controller.Refresh(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void Refresh_InvalidId_ReturnsBadRequest()
        {
            var result = _controller.Refresh(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Refresh_ReturnsMessage()
        {
            _serviceMock.Setup(s => s.RefreshAnalyticsDataset(1))
                        .Returns(CreateDto());

            var result = _controller.Refresh(1) as OkObjectResult;

            Assert.That(result.Value.ToString(), Does.Contain("Dataset refreshed successfully"));
        }

        #endregion
    }
}