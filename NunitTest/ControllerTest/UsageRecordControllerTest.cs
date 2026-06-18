using NUnit.Framework;
using Moq;
using Telecomm360.Controllers;
using Telecomm360.DTOs;
using Telecomm360.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class UsageRecordControllerTests
    {
        private Mock<IUsageRecordService> _serviceMock;
        private UsageRecordController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IUsageRecordService>();
            _controller = new UsageRecordController(_serviceMock.Object);
        }

        #region Helpers

        private UsageRecordDto CreateRecord(int UsageRecordId = 1)
        {
            return new UsageRecordDto
            {
                UsageRecordId = UsageRecordId,
                SubscriberID = 1,
                // Add other properties if needed
            };
        }

        #endregion

        #region GetAllUsageRecords

        [Test]
        public void GetAllUsageRecords_Valid_ReturnsOk()
        {
            // Arrange
            var records = new List<UsageRecordDto>
            {
                CreateRecord(1),
                CreateRecord(2)
            };

            _serviceMock.Setup(s => s.GetAllUsageRecord(It.IsAny<UsageRecordDto>()))
                        .Returns(records);

            // Act
            var result = _controller.GetAllUsageRecords();

            // Assert
            var ok = result.Result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(records));
        }

        #endregion

        #region GetUsageRecordById

        [Test]
        public void GetUsageRecordById_Valid_ReturnsOk()
        {
            // Arrange
            var record = CreateRecord(1);

            _serviceMock.Setup(s => s.GetUsageRecordById(It.IsAny<UsageRecordDto>()))
                        .Returns(record);

            // Act
            var result = _controller.GetUsageRecordById(1);

            // Assert
            var ok = result.Result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(record));
        }

        [Test]
        public void GetUsageRecordById_NotFound_Returns404()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetUsageRecordById(It.IsAny<UsageRecordDto>()))
                        .Returns((UsageRecordDto)null);

            // Act
            var result = _controller.GetUsageRecordById(1);

            // Assert
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        #endregion

        #region GetUsageRecordsBySubscriber

        [Test]
        public void GetUsageRecordsBySubscriber_Valid_ReturnsOk()
        {
            // Arrange
            var records = new List<UsageRecordDto>
            {
                CreateRecord(1),
                CreateRecord(2)
            };

            _serviceMock.Setup(s => s.GetUsageRecordBySubscriber(It.IsAny<UsageRecordDto>()))
                        .Returns(records);

            // Act
            var result = _controller.GetUsageRecordsBySubscriber(1);

            // Assert
            var ok = result.Result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(records));
        }

        #endregion

        #region CreateUsageRecord

        [Test]
        public void CreateUsageRecord_Valid_ReturnsCreated()
        {
            // Arrange
            var request = CreateRecord(1);

            _serviceMock.Setup(s => s.CreateUsageRecord(request))
                        .Returns(request);

            // Act
            var result = _controller.CreateUsageRecord(request);

            // Assert
            var created = result.Result as CreatedAtActionResult;
            Assert.That(created, Is.Not.Null);
            Assert.That(created.Value, Is.EqualTo(request));
        }

        #endregion
    }
}