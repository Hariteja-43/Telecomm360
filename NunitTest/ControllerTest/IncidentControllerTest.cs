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

        #region GetIncidents

        [Test]
        public async Task GetIncidents_Valid_ReturnsOk()
        {
            var response = new List<IncidentResponse> { CreateIncident() };

            _serviceMock.Setup(s => s.GetIncidentsAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync(response);

            var result = await _controller.GetIncidents(CreateSearchDto());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetIncidents_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.GetIncidents(CreateSearchDto());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetIncidents_EmptyList_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetIncidentsAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync(new List<IncidentResponse>());

            var result = await _controller.GetIncidents(CreateSearchDto());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetIncidents_ServiceCalled_Once()
        {
            _serviceMock.Setup(s => s.GetIncidentsAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync(new List<IncidentResponse>());

            await _controller.GetIncidents(CreateSearchDto());

            _serviceMock.Verify(s => s.GetIncidentsAsync(It.IsAny<SearchDtos>()), Times.Once);
        }

        [Test]
        public async Task GetIncidents_ReturnsCorrectData()
        {
            var response = new List<IncidentResponse> { CreateIncident() };

            _serviceMock.Setup(s => s.GetIncidentsAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync(response);

            var result = await _controller.GetIncidents(CreateSearchDto()) as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task GetIncidents_ServiceReturnsNull_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetIncidentsAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync((List<IncidentResponse>)null);

            var result = await _controller.GetIncidents(CreateSearchDto());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        #endregion

        #region CreateIncident

        [Test]
        public async Task CreateIncident_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CreateIncidentAsync(It.IsAny<IncidentCreateRequest>()))
                        .ReturnsAsync(CreateIncident());

            var result = await _controller.CreateIncident(CreateIncidentRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateIncident_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.CreateIncident(CreateIncidentRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task CreateIncident_ResponseNotNull_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CreateIncidentAsync(It.IsAny<IncidentCreateRequest>()))
                        .ReturnsAsync(CreateIncident());

            var result = await _controller.CreateIncident(CreateIncidentRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateIncident_ServiceCalled_Once()
        {
            _serviceMock.Setup(s => s.CreateIncidentAsync(It.IsAny<IncidentCreateRequest>()))
                        .ReturnsAsync(CreateIncident());

            await _controller.CreateIncident(CreateIncidentRequest());

            _serviceMock.Verify(s => s.CreateIncidentAsync(It.IsAny<IncidentCreateRequest>()), Times.Once);
        }

        [Test]
        public async Task CreateIncident_ServiceReturnsNull_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CreateIncidentAsync(It.IsAny<IncidentCreateRequest>()))
                        .ReturnsAsync((IncidentResponse)null);

            var result = await _controller.CreateIncident(CreateIncidentRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateIncident_NullRequest_ReturnsOk()
        {
            var result = await _controller.CreateIncident(null);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        #endregion

        #region PatchIncident

        [Test]
        public async Task PatchIncident_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.PatchIncidentAsync(1, It.IsAny<IncidentPatchRequest>()))
                        .ReturnsAsync(CreateIncident());

            var result = await _controller.PatchIncident(1, CreatePatchRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task PatchIncident_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.PatchIncident(1, CreatePatchRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task PatchIncident_NullResponse_ReturnsOkWithNull()
        {
            _serviceMock.Setup(s => s.PatchIncidentAsync(1, It.IsAny<IncidentPatchRequest>()))
                        .ReturnsAsync((IncidentResponse)null);

            var result = await _controller.PatchIncident(1, CreatePatchRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task PatchIncident_InvalidId_ReturnsOk()
        {
            var result = await _controller.PatchIncident(0, CreatePatchRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task PatchIncident_ServiceCalled_Once()
        {
            _serviceMock.Setup(s => s.PatchIncidentAsync(1, It.IsAny<IncidentPatchRequest>()))
                        .ReturnsAsync(CreateIncident());

            await _controller.PatchIncident(1, CreatePatchRequest());

            _serviceMock.Verify(s => s.PatchIncidentAsync(1, It.IsAny<IncidentPatchRequest>()), Times.Once);
        }

        [Test]
        public async Task PatchIncident_ReturnsCorrectData()
        {
            var response = CreateIncident();

            _serviceMock.Setup(s => s.PatchIncidentAsync(1, It.IsAny<IncidentPatchRequest>()))
                        .ReturnsAsync(response);

            var result = await _controller.PatchIncident(1, CreatePatchRequest()) as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(response));
        }

        #endregion
    }
}