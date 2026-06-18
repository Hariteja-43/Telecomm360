using NUnit.Framework;
using Moq;
using Telecomm360.Controllers;
using Telecomm360.Service.Interface;
using Telecomm360.DTO;
using Telecomm360.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class IncidentsControllerTests
    {
        private Mock<IIncidentService> _serviceMock;
        private IncidentsController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IIncidentService>();
            _controller = new IncidentsController(_serviceMock.Object);
        }

        #region Helpers

        private SearchDtos CreateSearchDto()
        {
            return new SearchDtos();
        }

        private IncidentCreateRequest CreateIncidentRequest()
        {
            return new IncidentCreateRequest
            {
                TargetAlarmId = 100,
                IncidentPriority = "High"
            };
        }

        private IncidentPatchRequest CreatePatchRequest()
        {
            return new IncidentPatchRequest
            {
                UpdatedStatus = "Resolved",
                ResolutionDetails = "Issue fixed"
            };
        }

        private IncidentResponse CreateIncident()
        {
            return new IncidentResponse
            {
                DisplayId = 1,
                AssignedEngineer = "John",
                IncidentPriority = "High",
                CurrentStatus = "Open",
                ResolutionDetails = ""
            };
        }

        #endregion

        #region GetIncidents (3 Tests)

        [Test]
        public async Task GetIncidents_Valid_ReturnsOk()
        {
            // Arrange
            var response = new List<IncidentResponse> { CreateIncident() };

            _serviceMock.Setup(s => s.GetIncidentsAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.GetIncidents(CreateSearchDto());

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task GetIncidents_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "invalid");

            // Act
            var result = await _controller.GetIncidents(CreateSearchDto());

            // Assert
            var bad = result as BadRequestObjectResult;
            Assert.That(bad, Is.Not.Null);
            Assert.That(bad.Value, Is.EqualTo(MessageConstants.InvalidModel));
        }

        [Test]
        public async Task GetIncidents_EmptyList_ReturnsOk()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetIncidentsAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync(new List<IncidentResponse>());

            // Act
            var result = await _controller.GetIncidents(CreateSearchDto());

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(((List<IncidentResponse>)ok.Value).Count, Is.EqualTo(0));
        }

        #endregion

        #region CreateIncident (3 Tests)

        [Test]
        public async Task CreateIncident_Valid_ReturnsOk()
        {
            // Arrange
            var request = CreateIncidentRequest();
            var response = CreateIncident();

            _serviceMock.Setup(s => s.CreateIncidentAsync(It.IsAny<IncidentCreateRequest>()))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateIncident(request);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task CreateIncident_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "invalid");

            // Act
            var result = await _controller.CreateIncident(CreateIncidentRequest());

            // Assert
            var bad = result as BadRequestObjectResult;
            Assert.That(bad, Is.Not.Null);
            Assert.That(bad.Value, Is.EqualTo(MessageConstants.InvalidModel));
        }

        [Test]
        public async Task CreateIncident_ResponseNotNull_ReturnsOk()
        {
            // Arrange
            _serviceMock.Setup(s => s.CreateIncidentAsync(It.IsAny<IncidentCreateRequest>()))
                        .ReturnsAsync(CreateIncident());

            // Act
            var result = await _controller.CreateIncident(CreateIncidentRequest());

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        #endregion

        #region PatchIncident (3 Tests)

        [Test]
        public async Task PatchIncident_Valid_ReturnsOk()
        {
            // Arrange
            var request = CreatePatchRequest();
            var response = CreateIncident();

            _serviceMock.Setup(s => s.PatchIncidentAsync(1, It.IsAny<IncidentPatchRequest>()))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.PatchIncident(1, request);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task PatchIncident_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "invalid");

            // Act
            var result = await _controller.PatchIncident(1, CreatePatchRequest());

            // Assert
            var bad = result as BadRequestObjectResult;
            Assert.That(bad, Is.Not.Null);
            Assert.That(bad.Value, Is.EqualTo(MessageConstants.InvalidModel));
        }

        [Test]
        public async Task PatchIncident_NullResponse_ReturnsOkWithNull()
        {
            // Arrange
            _serviceMock.Setup(s => s.PatchIncidentAsync(1, It.IsAny<IncidentPatchRequest>()))
                        .ReturnsAsync((IncidentResponse)null);

            // Act
            var result = await _controller.PatchIncident(1, CreatePatchRequest());

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.Null);
        }

        #endregion
    }
}