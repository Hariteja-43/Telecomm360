using NUnit.Framework;
using Moq;
using Telecomm360.Controllers;
using Telecomm360.Service.Interface;
using Telecomm360.DTO;
using Telecomm360.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class AlarmControllerTests
    {
        private Mock<IAlarmService> _serviceMock;
        private AlarmController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IAlarmService>();
            _controller = new AlarmController(_serviceMock.Object);
        }

        #region Helpers

        private SearchDtos CreateSearchDto() => new SearchDtos();

        private AlarmCreateRequest CreateCreateRequest()
        {
            return new AlarmCreateRequest
            {
                SourceNode = "Node-1",
                FaultSeverity = "Critical"
            };
        }

        private AlarmResponse CreateAlarm()
        {
            return new AlarmResponse
            {
                DisplayId = 1,
                SourceNode = "Node-1",
                FaultSeverity = "Critical"
            };
        }

        private AlarmSummaryResponse CreateSummary()
        {
            return new AlarmSummaryResponse
            {
                TotalCritical = 5,
                TotalMajor = 3,
                TotalMinor = 1,
                TotalWarning = 2
            };
        }

        #endregion

        #region GetAlarms

        [Test]
        public async Task GetAlarms_ValidModel_ReturnsOk()
        {
            var response = new List<AlarmResponse> { CreateAlarm() };

            _serviceMock.Setup(s => s.GetAlarmsAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync(response);

            var result = await _controller.GetAlarms(CreateSearchDto());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAlarms_ValidModel_ReturnsCorrectData()
        {
            var response = new List<AlarmResponse> { CreateAlarm() };

            _serviceMock.Setup(s => s.GetAlarmsAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync(response);

            var result = await _controller.GetAlarms(CreateSearchDto()) as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task GetAlarms_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.GetAlarms(CreateSearchDto());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetAlarms_ServiceCalled_Once()
        {
            _serviceMock.Setup(s => s.GetAlarmsAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync(new List<AlarmResponse>());

            await _controller.GetAlarms(CreateSearchDto());

            _serviceMock.Verify(s => s.GetAlarmsAsync(It.IsAny<SearchDtos>()), Times.Once);
        }

        [Test]
        public async Task GetAlarms_EmptyList_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAlarmsAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync(new List<AlarmResponse>());

            var result = await _controller.GetAlarms(CreateSearchDto());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAlarms_ServiceReturnsNull_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAlarmsAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync((List<AlarmResponse>)null);

            var result = await _controller.GetAlarms(CreateSearchDto());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        #endregion

        #region GetAlarmsSummary

        [Test]
        public async Task GetAlarmsSummary_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAlarmsSummaryAsync())
                        .ReturnsAsync(CreateSummary());

            var result = await _controller.GetAlarmsSummary();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAlarmsSummary_ReturnsCorrectData()
        {
            var summary = CreateSummary();

            _serviceMock.Setup(s => s.GetAlarmsSummaryAsync())
                        .ReturnsAsync(summary);

            var result = await _controller.GetAlarmsSummary() as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(summary));
        }

        [Test]
        public async Task GetAlarmsSummary_ServiceCalled_Once()
        {
            _serviceMock.Setup(s => s.GetAlarmsSummaryAsync())
                        .ReturnsAsync(CreateSummary());

            await _controller.GetAlarmsSummary();

            _serviceMock.Verify(s => s.GetAlarmsSummaryAsync(), Times.Once);
        }

        [Test]
        public async Task GetAlarmsSummary_ServiceReturnsNull_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAlarmsSummaryAsync())
                        .ReturnsAsync((AlarmSummaryResponse)null);

            var result = await _controller.GetAlarmsSummary();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        #endregion

        #region CreateAlarm

        [Test]
        public async Task CreateAlarm_ValidModel_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CreateAlarmAsync(It.IsAny<AlarmCreateRequest>()))
                        .ReturnsAsync(CreateAlarm());

            var result = await _controller.CreateAlarm(CreateCreateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateAlarm_ValidModel_ReturnsCorrectData()
        {
            var alarm = CreateAlarm();

            _serviceMock.Setup(s => s.CreateAlarmAsync(It.IsAny<AlarmCreateRequest>()))
                        .ReturnsAsync(alarm);

            var result = await _controller.CreateAlarm(CreateCreateRequest()) as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(alarm));
        }

        [Test]
        public async Task CreateAlarm_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.CreateAlarm(CreateCreateRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task CreateAlarm_ServiceCalled_Once()
        {
            _serviceMock.Setup(s => s.CreateAlarmAsync(It.IsAny<AlarmCreateRequest>()))
                        .ReturnsAsync(CreateAlarm());

            await _controller.CreateAlarm(CreateCreateRequest());

            _serviceMock.Verify(s => s.CreateAlarmAsync(It.IsAny<AlarmCreateRequest>()), Times.Once);
        }

        [Test]
        public async Task CreateAlarm_ServiceReturnsNull_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CreateAlarmAsync(It.IsAny<AlarmCreateRequest>()))
                        .ReturnsAsync((AlarmResponse)null);

            var result = await _controller.CreateAlarm(CreateCreateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateAlarm_NullRequest_ReturnsOk()
        {
            // Controller does not null-guard request; it relies on ModelState.
            var result = await _controller.CreateAlarm(null);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        #endregion
    }
}