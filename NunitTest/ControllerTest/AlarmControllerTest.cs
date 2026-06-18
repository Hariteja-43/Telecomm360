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

        private SearchDtos CreateSearchDto()
        {
            return new SearchDtos();
        }

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
                DisplayId = 1, // ensure correct type
                SourceNode = "Node-1",
                FaultSeverity = "Critical"
            };
        }

        // FIXED HERE
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
            // Arrange
            var search = CreateSearchDto();
            var response = new List<AlarmResponse> { CreateAlarm() };

            _serviceMock.Setup(s => s.GetAlarmsAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAlarms(search);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task GetAlarms_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "invalid");

            // Act
            var result = await _controller.GetAlarms(CreateSearchDto());

            // Assert
            var badRequest = result as BadRequestObjectResult;
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.Value, Is.EqualTo(MessageConstants.InvalidModel));
        }

        #endregion

        #region GetAlarmsSummary

        [Test]
        public async Task GetAlarmsSummary_Valid_ReturnsOk()
        {
            // Arrange
            var summary = CreateSummary();

            _serviceMock.Setup(s => s.GetAlarmsSummaryAsync())
                        .ReturnsAsync(summary);

            // Act
            var result = await _controller.GetAlarmsSummary();

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(summary));
        }

        #endregion

        #region CreateAlarm

        [Test]
        public async Task CreateAlarm_ValidModel_ReturnsOk()
        {
            // Arrange
            var request = CreateCreateRequest();
            var response = CreateAlarm();

            _serviceMock.Setup(s => s.CreateAlarmAsync(It.IsAny<AlarmCreateRequest>()))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateAlarm(request);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task CreateAlarm_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "invalid");

            // Act
            var result = await _controller.CreateAlarm(CreateCreateRequest());

            // Assert
            var badRequest = result as BadRequestObjectResult;
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.Value, Is.EqualTo(MessageConstants.InvalidModel));
        }

        #endregion
    }
}