using NUnit.Framework;
using Moq;
using Telecomm360.Controllers;
using Telecomm360.DTO;
using Telecomm360.Service.Interface;
using Telecomm360.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class NetworkResourcesControllerTests
    {
        private Mock<INetworkResourceService> _serviceMock;
        private NetworkResourcesController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<INetworkResourceService>();
            _controller = new NetworkResourcesController(_serviceMock.Object);
        }

        #region Helpers

        private CreateNetworkResourceRequestDto CreateRequest()
        {
            return new CreateNetworkResourceRequestDto
            {
                NetworkResourceType = "SIM",
                Location = "Chennai",
                Capacity = 100,
                Status = Status.Active   // Make sure enum exists
            };
        }

        private NetworkResourceResponseDto CreateResponse(int id = 1)
        {
            return new NetworkResourceResponseDto
            {
                NetworkResourceId = id,
                NetworkResourceType = "SIM",
                Location = "Chennai",
                Capacity = 100,
                AllocatedTo = null,
                Status = Status.Active
            };
        }

        #endregion

        #region CreateNetworkResource (3 Tests)

        [Test]
        public async Task CreateNetworkResource_Valid_ReturnsOk()
        {
            var response = CreateResponse();

            _serviceMock.Setup(s => s.CreateResourceAsync(It.IsAny<CreateNetworkResourceRequestDto>()))
                        .ReturnsAsync(response);

            var result = await _controller.CreateNetworkResource(CreateRequest());

            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test]
        public async Task CreateNetworkResource_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.CreateNetworkResource(CreateRequest());

            var bad = result as BadRequestObjectResult;
            Assert.That(bad, Is.Not.Null);
            Assert.That(bad.Value, Is.EqualTo(GeneralConstants.InvalidInput));
        }

        [Test]
        public async Task CreateNetworkResource_ResponseReturned_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CreateResourceAsync(It.IsAny<CreateNetworkResourceRequestDto>()))
                        .ReturnsAsync(CreateResponse());

            var result = await _controller.CreateNetworkResource(CreateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        #endregion

        #region GetAllNetworkResource (3 Tests)

        [Test]
        public async Task GetAllNetworkResource_Valid_ReturnsOk()
        {
            var list = new List<NetworkResourceResponseDto>
            {
                CreateResponse(1),
                CreateResponse(2)
            };

            _serviceMock.Setup(s => s.GetAllResourcesAsync())
                        .ReturnsAsync(list);

            var result = await _controller.GetAllNetworkResource();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllNetworkResource_Empty_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllResourcesAsync())
                        .ReturnsAsync(new List<NetworkResourceResponseDto>());

            var result = await _controller.GetAllNetworkResource();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllNetworkResource_ServiceCalled()
        {
            _serviceMock.Setup(s => s.GetAllResourcesAsync())
                        .ReturnsAsync(new List<NetworkResourceResponseDto>());

            await _controller.GetAllNetworkResource();

            _serviceMock.Verify(s => s.GetAllResourcesAsync(), Times.Once);
        }

        #endregion

        #region GetNetworkResourceById (3 Tests)

        [Test]
        public async Task GetById_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetResourceByIdAsync(1))
                        .ReturnsAsync(CreateResponse());

            var result = await _controller.GetNetworkResourceById(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_Invalid_ReturnsBadRequest()
        {
            var result = await _controller.GetNetworkResourceById(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetById_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.GetResourceByIdAsync(1))
                        .ReturnsAsync((NetworkResourceResponseDto)null);

            var result = await _controller.GetNetworkResourceById(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        #endregion

        #region UpdateNetworkResourceById (3 Tests)

        [Test]
        public async Task Update_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.UpdateResourceAsync(1, It.IsAny<CreateNetworkResourceRequestDto>()))
                        .ReturnsAsync(CreateResponse());

            var result = await _controller.UpdateNetworkResourceById(1, CreateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task Update_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.UpdateNetworkResourceById(0, CreateRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task Update_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.UpdateResourceAsync(1, It.IsAny<CreateNetworkResourceRequestDto>()))
                        .ReturnsAsync((NetworkResourceResponseDto)null);

            var result = await _controller.UpdateNetworkResourceById(1, CreateRequest());

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        #endregion

        #region DeleteNetworkResourceById (3 Tests)

        [Test]
        public async Task Delete_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.DeleteResourceAsync(1))
                        .ReturnsAsync(true);

            var result = await _controller.DeleteNetworkResourceById(1);

            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public async Task Delete_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.DeleteNetworkResourceById(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task Delete_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.DeleteResourceAsync(1))
                        .ReturnsAsync(false);

            var result = await _controller.DeleteNetworkResourceById(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        #endregion
    }
}
