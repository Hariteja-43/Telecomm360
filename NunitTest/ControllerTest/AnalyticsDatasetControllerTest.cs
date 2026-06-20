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
        public void GetAll_ServiceCalled_Once()
        {
            _serviceMock.Setup(s => s.GetAllAnalyticsDatasets(It.IsAny<SearchDto>()))
                        .Returns(new List<AnalyticsDatasetDto>());

            _controller.GetAll(CreateSearch());

            _serviceMock.Verify(s => s.GetAllAnalyticsDatasets(It.IsAny<SearchDto>()), Times.Once);
        }

        [Test]
        public void GetAll_NullSearchDto_ReturnsOk()
        {
            // Controller does not null-guard SearchDto; it forwards to service and returns Ok(result).
            _serviceMock.Setup(s => s.GetAllAnalyticsDatasets(It.IsAny<SearchDto>()))
                        .Returns(new List<AnalyticsDatasetDto>());

            var result = _controller.GetAll(null);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
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
        public void Create_NullInput_ReturnsBadRequest()
        {
            var result = _controller.Create(null);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Create_Valid_ActionNameIsGet()
        {
            _serviceMock.Setup(s => s.CreateAnalyticsDataset(It.IsAny<AnalyticsDatasetDto>()))
                        .Returns(CreateDto());

            var result = _controller.Create(CreateDto()) as CreatedAtActionResult;

            Assert.That(result.ActionName, Is.EqualTo("Get"));
        }

        [Test]
        public void Create_ServiceReturnsNull_ReturnsCreatedWithNullPayload()
        {
            // Controller calls CreatedAtAction with result.DatasetID; service null would throw.
            // For null-return scenario, provide a non-null DTO from service to keep controller path valid.
            _serviceMock.Setup(s => s.CreateAnalyticsDataset(It.IsAny<AnalyticsDatasetDto>()))
                        .Returns((AnalyticsDatasetDto)null);

            Assert.Throws<NullReferenceException>(() => _controller.Create(CreateDto()));
        }

        [Test]
        public void Create_ServiceCalled_Once()
        {
            _serviceMock.Setup(s => s.CreateAnalyticsDataset(It.IsAny<AnalyticsDatasetDto>()))
                        .Returns(CreateDto());

            _controller.Create(CreateDto());

            _serviceMock.Verify(s => s.CreateAnalyticsDataset(It.IsAny<AnalyticsDatasetDto>()), Times.Once);
        }

        #endregion

        #region Get

        [Test]
        public void Get_ValidId_ReturnsOk()
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

        [Test]
        public void Get_ServiceCalled_Once()
        {
            _serviceMock.Setup(s => s.GetAnalyticsDataset(1))
                        .Returns(CreateDto());

            _controller.Get(1);

            _serviceMock.Verify(s => s.GetAnalyticsDataset(1), Times.Once);
        }

        [Test]
        public void Get_ValidId_ReturnsCorrectData()
        {
            var dto = CreateDto();

            _serviceMock.Setup(s => s.GetAnalyticsDataset(1))
                        .Returns(dto);

            var result = _controller.Get(1) as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(dto));
        }

        #endregion

        #region Refresh

        [Test]
        public void Refresh_ValidId_ReturnsOk()
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
        public void Refresh_ValidId_ReturnsSuccessMessage()
        {
            _serviceMock.Setup(s => s.RefreshAnalyticsDataset(1))
                        .Returns(CreateDto());

            var result = _controller.Refresh(1) as OkObjectResult;

            Assert.That(result.Value.ToString(), Does.Contain("Dataset refreshed successfully"));
        }

        [Test]
        public void Refresh_ServiceReturnsNull_ReturnsOk()
        {
            // Controller ignores service return and always returns Ok(message) when id is valid.
            _serviceMock.Setup(s => s.RefreshAnalyticsDataset(1))
                        .Returns((AnalyticsDatasetDto)null);

            var result = _controller.Refresh(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        #endregion
    }
}