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

        private UsageRecordDto CreateRecord(int id = 1)
        {
            return new UsageRecordDto
            {
                UsageRecordId = id,
                SubscriberID = 1
            };
        }

        #endregion

        #region GetAllUsageRecords

[Test]
        public void GetAllUsageRecords_Valid_ReturnsOk()
        {
            var records = new List<UsageRecordDto> { CreateRecord() };

            _serviceMock.Setup(s => s.GetAllUsageRecord(It.IsAny<UsageRecordDto>()))
                        .Returns(records);

            var result = _controller.GetAllUsageRecords();

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetAllUsageRecords_ReturnsCorrectData()
        {
            var records = new List<UsageRecordDto> { CreateRecord() };

            _serviceMock.Setup(s => s.GetAllUsageRecord(It.IsAny<UsageRecordDto>()))
                        .Returns(records);

            var result = _controller.GetAllUsageRecords();

            var ok = result.Result as OkObjectResult;

            Assert.That(ok.Value, Is.EqualTo(records));
        }

        [Test]
        public void GetAllUsageRecords_Empty_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllUsageRecord(It.IsAny<UsageRecordDto>()))
                        .Returns(new List<UsageRecordDto>());

            var result = _controller.GetAllUsageRecords();

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetAllUsageRecords_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetAllUsageRecord(It.IsAny<UsageRecordDto>()))
                        .Returns(new List<UsageRecordDto>());

            _controller.GetAllUsageRecords();

            _serviceMock.Verify(s => s.GetAllUsageRecord(It.IsAny<UsageRecordDto>()), Times.Once);
        }

        #endregion

        #region GetUsageRecordById

        [Test]
        public void GetUsageRecordById_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetUsageRecordById(It.IsAny<UsageRecordDto>()))
                        .Returns(CreateRecord());

            var result = _controller.GetUsageRecordById(1);

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetUsageRecordById_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.GetUsageRecordById(It.IsAny<UsageRecordDto>()))
                        .Returns((UsageRecordDto)null);

            var result = _controller.GetUsageRecordById(1);

            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void GetUsageRecordById_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetUsageRecordById(It.IsAny<UsageRecordDto>()))
                        .Returns(CreateRecord());

            _controller.GetUsageRecordById(1);

            _serviceMock.Verify(s => s.GetUsageRecordById(It.IsAny<UsageRecordDto>()), Times.Once);
        }

        #endregion

        #region GetUsageRecordsBySubscriber

        [Test]
        public void GetUsageRecordsBySubscriber_Valid_ReturnsOk()
        {
            var records = new List<UsageRecordDto> { CreateRecord() };

            _serviceMock.Setup(s => s.GetUsageRecordBySubscriber(It.IsAny<UsageRecordDto>()))
                        .Returns(records);

            var result = _controller.GetUsageRecordsBySubscriber(1);

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetUsageRecordsBySubscriber_Empty_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetUsageRecordBySubscriber(It.IsAny<UsageRecordDto>()))
                        .Returns(new List<UsageRecordDto>());

            var result = _controller.GetUsageRecordsBySubscriber(1);

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetUsageRecordsBySubscriber_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetUsageRecordBySubscriber(It.IsAny<UsageRecordDto>()))
                        .Returns(new List<UsageRecordDto>());

            _controller.GetUsageRecordsBySubscriber(1);

            _serviceMock.Verify(s => s.GetUsageRecordBySubscriber(It.IsAny<UsageRecordDto>()), Times.Once);
        }

        #endregion

        #region CreateUsageRecord

        [Test]
        public void CreateUsageRecord_Valid_ReturnsCreated()
        {
            var request = CreateRecord();

            _serviceMock.Setup(s => s.CreateUsageRecord(request))
                        .Returns(request);

            var result = _controller.CreateUsageRecord(request);

            Assert.That(result.Result, Is.TypeOf<CreatedAtActionResult>());
        }

        [Test]
        public void CreateUsageRecord_ReturnsCorrectData()
        {
            var request = CreateRecord();

            _serviceMock.Setup(s => s.CreateUsageRecord(request))
                        .Returns(request);

            var result = _controller.CreateUsageRecord(request);

            var created = result.Result as CreatedAtActionResult;

            Assert.That(created.Value, Is.EqualTo(request));
        }

        [Test]
        public void CreateUsageRecord_ServiceCalledOnce()
        {
            var request = CreateRecord();

            _serviceMock.Setup(s => s.CreateUsageRecord(request))
                        .Returns(request);

            _controller.CreateUsageRecord(request);

            _serviceMock.Verify(s => s.CreateUsageRecord(request), Times.Once);
        }

        #endregion
    }
}