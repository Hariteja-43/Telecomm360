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
    public class ProvisioningTasksControllerTests
    {
        private Mock<IProvisioningTaskService> _serviceMock;
        private ProvisioningTasksController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IProvisioningTaskService>();
            _controller = new ProvisioningTasksController(_serviceMock.Object);
        }

        #region Helpers

        private CreateProvisioningTaskRequestDto CreateRequest()
        {
            return new CreateProvisioningTaskRequestDto
            {
                OrderId = 100,
                SubscriberId = 200,
                MSISDN = "9876543210",
                ResourceType = "SIM"
            };
        }

        // FIXED: matches your DTO
        private ProvisioningTaskResponseDto CreateResponse(int id = 1)
        {
            return new ProvisioningTaskResponseDto
            {
                ProvisioningTaskId = id,
                OrderId = 100,
                SubscriberId = 200,
                MSISDN = "9876543210",
                ResourceType = "SIM",
                Status = Status.Pending   // use your enum value
            };
        }

        #endregion

        #region CreateProvisioningTasks

        [Test]
        public async Task CreateProvisioningTasks_Valid_ReturnsOk()
        {
            var request = CreateRequest();
            var response = CreateResponse(1);

            _serviceMock.Setup(s => s.CreateTaskAsync(request))
                        .ReturnsAsync(response);

            var result = await _controller.CreateProvisioningTasks(request);

            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.Not.Null);
        }

        [Test]
        public async Task CreateProvisioningTasks_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.CreateProvisioningTasks(CreateRequest());

            var badRequest = result as BadRequestObjectResult;
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.Value, Is.EqualTo(GeneralConstants.InvalidInput));
        }

        #endregion

        #region GetAllProvisioningTasks

        [Test]
        public async Task GetAllProvisioningTasks_Valid_ReturnsOk()
        {
            var list = new List<ProvisioningTaskResponseDto>
            {
                CreateResponse(1),
                CreateResponse(2)
            };

            _serviceMock.Setup(s => s.GetAllTasksAsync())
                        .ReturnsAsync(list);

            var result = await _controller.GetAllProvisioningTasks();

            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.Not.Null);
        }

        #endregion

        #region GetProvisioningTasksById

        [Test]
        public async Task GetProvisioningTasksById_Valid_ReturnsOk()
        {
            var response = CreateResponse(1);

            _serviceMock.Setup(s => s.GetTaskByIdAsync(1))
                        .ReturnsAsync(response);

            var result = await _controller.GetProvisioningTasksById(1);

            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test]
        public async Task GetProvisioningTasksById_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.GetProvisioningTasksById(0);

            var badRequest = result as BadRequestObjectResult;
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.Value, Is.EqualTo(GeneralConstants.InvalidInput));
        }

        [Test]
        public async Task GetProvisioningTasksById_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.GetTaskByIdAsync(1))
                        .ReturnsAsync((ProvisioningTaskResponseDto)null);

            var result = await _controller.GetProvisioningTasksById(1);

            var notFound = result as NotFoundObjectResult;
            Assert.That(notFound, Is.Not.Null);
            Assert.That(notFound.Value, Is.EqualTo(ProvisioningTaskConstants.NotFound));
        }

        #endregion

        #region UpdateProvisioningTasks

        [Test]
        public async Task UpdateProvisioningTasks_Valid_ReturnsOk()
        {
            var request = CreateRequest();
            var response = CreateResponse(1);

            _serviceMock.Setup(s => s.UpdateTaskAsync(1, request))
                        .ReturnsAsync(response);

            var result = await _controller.UpdateProvisioningTasks(1, request);

            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test]
        public async Task UpdateProvisioningTasks_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.UpdateProvisioningTasks(0, CreateRequest());

            var badRequest = result as BadRequestObjectResult;
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.Value, Is.EqualTo(GeneralConstants.InvalidInput));
        }

        [Test]
        public async Task UpdateProvisioningTasks_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.UpdateTaskAsync(1, It.IsAny<CreateProvisioningTaskRequestDto>()))
                        .ReturnsAsync((ProvisioningTaskResponseDto)null);

            var result = await _controller.UpdateProvisioningTasks(1, CreateRequest());

            var notFound = result as NotFoundObjectResult;
            Assert.That(notFound, Is.Not.Null);
        }

        #endregion

        #region DeleteProvisioningTasks

        [Test]
        public async Task DeleteProvisioningTasks_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.DeleteTaskAsync(1))
                        .ReturnsAsync(true);

            var result = await _controller.DeleteProvisioningTasks(1);

            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public async Task DeleteProvisioningTasks_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.DeleteProvisioningTasks(0);

            var badRequest = result as BadRequestObjectResult;
            Assert.That(badRequest, Is.Not.Null);
        }

        [Test]
        public async Task DeleteProvisioningTasks_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.DeleteTaskAsync(1))
                        .ReturnsAsync(false);

            var result = await _controller.DeleteProvisioningTasks(1);

            var notFound = result as NotFoundObjectResult;
            Assert.That(notFound, Is.Not.Null);
        }

        #endregion
    }
}