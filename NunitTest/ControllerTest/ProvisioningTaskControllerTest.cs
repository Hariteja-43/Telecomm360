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

        private ProvisioningTaskResponseDto CreateResponse(int id = 1)
        {
            return new ProvisioningTaskResponseDto
            {
                ProvisioningTaskId = id,
                OrderId = 100,
                SubscriberId = 200,
                MSISDN = "9876543210",
                ResourceType = "SIM",
                Status = Status.Pending
            };
        }

        #endregion

        #region CreateProvisioningTasks

        [Test]
        public async Task CreateProvisioningTasks_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CreateTaskAsync(It.IsAny<CreateProvisioningTaskRequestDto>()))
                        .ReturnsAsync(CreateResponse());

            var result = await _controller.CreateProvisioningTasks(CreateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateProvisioningTasks_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.CreateProvisioningTasks(CreateRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task CreateProvisioningTasks_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.CreateTaskAsync(It.IsAny<CreateProvisioningTaskRequestDto>()))
                        .ReturnsAsync(CreateResponse());

            await _controller.CreateProvisioningTasks(CreateRequest());

            _serviceMock.Verify(s => s.CreateTaskAsync(It.IsAny<CreateProvisioningTaskRequestDto>()), Times.Once);
        }

        [Test]
        public async Task CreateProvisioningTasks_NullRequest_ReturnsOk()
        {
            var result = await _controller.CreateProvisioningTasks(null);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        #endregion

        #region GetAllProvisioningTasks

        [Test]
        public async Task GetAllProvisioningTasks_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllTasksAsync())
                        .ReturnsAsync(new List<ProvisioningTaskResponseDto> { CreateResponse() });

            var result = await _controller.GetAllProvisioningTasks();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllProvisioningTasks_Empty_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllTasksAsync())
                        .ReturnsAsync(new List<ProvisioningTaskResponseDto>());

            var result = await _controller.GetAllProvisioningTasks();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllProvisioningTasks_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetAllTasksAsync())
                        .ReturnsAsync(new List<ProvisioningTaskResponseDto>());

            await _controller.GetAllProvisioningTasks();

            _serviceMock.Verify(s => s.GetAllTasksAsync(), Times.Once);
        }

        [Test]
        public async Task GetAllProvisioningTasks_ReturnsCorrectData()
        {
            var data = new List<ProvisioningTaskResponseDto> { CreateResponse() };

            _serviceMock.Setup(s => s.GetAllTasksAsync())
                        .ReturnsAsync(data);

            var result = await _controller.GetAllProvisioningTasks() as OkObjectResult;

            var wrapped = result.Value as dynamic;
            Assert.That(wrapped.data, Is.EqualTo(data));
        }

        #endregion

        #region GetProvisioningTasksById

        [Test]
        public async Task GetProvisioningTasksById_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetTaskByIdAsync(1))
                        .ReturnsAsync(CreateResponse());

            var result = await _controller.GetProvisioningTasksById(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetProvisioningTasksById_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.GetProvisioningTasksById(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetProvisioningTasksById_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.GetTaskByIdAsync(1))
                        .ReturnsAsync((ProvisioningTaskResponseDto)null);

            var result = await _controller.GetProvisioningTasksById(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetProvisioningTasksById_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetTaskByIdAsync(1))
                        .ReturnsAsync(CreateResponse());

            await _controller.GetProvisioningTasksById(1);

            _serviceMock.Verify(s => s.GetTaskByIdAsync(1), Times.Once);
        }

        #endregion

        #region UpdateProvisioningTasks

        [Test]
        public async Task UpdateProvisioningTasks_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.UpdateTaskAsync(1, It.IsAny<CreateProvisioningTaskRequestDto>()))
                        .ReturnsAsync(CreateResponse());

            var result = await _controller.UpdateProvisioningTasks(1, CreateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task UpdateProvisioningTasks_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.UpdateProvisioningTasks(0, CreateRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task UpdateProvisioningTasks_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.UpdateTaskAsync(1, It.IsAny<CreateProvisioningTaskRequestDto>()))
                        .ReturnsAsync((ProvisioningTaskResponseDto)null);

            var result = await _controller.UpdateProvisioningTasks(1, CreateRequest());

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task UpdateProvisioningTasks_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.UpdateTaskAsync(1, It.IsAny<CreateProvisioningTaskRequestDto>()))
                        .ReturnsAsync(CreateResponse());

            await _controller.UpdateProvisioningTasks(1, CreateRequest());

            _serviceMock.Verify(s => s.UpdateTaskAsync(1, It.IsAny<CreateProvisioningTaskRequestDto>()), Times.Once);
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

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task DeleteProvisioningTasks_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.DeleteTaskAsync(1))
                        .ReturnsAsync(false);

            var result = await _controller.DeleteProvisioningTasks(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        #endregion
    }
}