using NUnit.Framework;
using Moq;
using Telecomm360.Controllers;
using Telecomm360.Service.Interface;
using Telecomm360.DTO;
using Telecomm360.DTOs;
using Telecomm360.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class RolesControllerTests
    {
        private Mock<IRoleService> _serviceMock;
        private RolesController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IRoleService>();
            _controller = new RolesController(_serviceMock.Object);
        }

        #region Helpers

        private SearchDtos CreateSearchDto() => new SearchDtos();

        private RoleCreateRequest CreateCreateRequest()
        {
            return new RoleCreateRequest
            {
                RoleName = "Admin",
                ConfigurationDescription = "Full access role"
            };
        }

        private RoleUpdateRequest CreateUpdateRequest()
        {
            return new RoleUpdateRequest
            {
                RoleName = "Updated Role",
                ConfigurationDescription = "Updated description"
            };
        }

        private RoleStatusPatchRequest CreatePatchRequest()
        {
            return new RoleStatusPatchRequest
            {
                SystemStatus = "Active"
            };
        }

        private RoleResponse CreateRoleResponse()
        {
            return new RoleResponse
            {
                DisplayId = "R001",
                RoleName = "Admin",
                ConfigurationDescription = "Full access role",
                SystemStatus = "Active"
            };
        }

        #endregion

        #region GetRoles

        [Test]
        public async Task GetRoles_ValidModel_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetRolesAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync(new List<RoleResponse> { CreateRoleResponse() });

            var result = await _controller.GetRoles(CreateSearchDto());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetRoles_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.GetRoles(CreateSearchDto());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetRoles_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetRolesAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync(new List<RoleResponse>());

            await _controller.GetRoles(CreateSearchDto());

            _serviceMock.Verify(s => s.GetRolesAsync(It.IsAny<SearchDtos>()), Times.Once);
        }

        [Test]
        public async Task GetRoles_ReturnsCorrectData()
        {
            var data = new List<RoleResponse> { CreateRoleResponse() };

            _serviceMock.Setup(s => s.GetRolesAsync(It.IsAny<SearchDtos>()))
                        .ReturnsAsync(data);

            var result = await _controller.GetRoles(CreateSearchDto()) as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(data));
        }

        #endregion

        #region CreateRole

        [Test]
        public async Task CreateRole_ValidModel_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CreateRoleAsync(It.IsAny<RoleCreateRequest>()))
                        .ReturnsAsync(CreateRoleResponse());

            var result = await _controller.CreateRole(CreateCreateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateRole_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.CreateRole(CreateCreateRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task CreateRole_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.CreateRoleAsync(It.IsAny<RoleCreateRequest>()))
                        .ReturnsAsync(CreateRoleResponse());

            await _controller.CreateRole(CreateCreateRequest());

            _serviceMock.Verify(s => s.CreateRoleAsync(It.IsAny<RoleCreateRequest>()), Times.Once);
        }

        [Test]
        public async Task CreateRole_NullRequest_ReturnsOk()
        {
            var result = await _controller.CreateRole(null);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        #endregion

        #region UpdateRole

        [Test]
        public async Task UpdateRole_ValidModel_ReturnsOk()
        {
            _serviceMock.Setup(s => s.UpdateRoleAsync(1, It.IsAny<RoleUpdateRequest>()))
                        .ReturnsAsync(CreateRoleResponse());

            var result = await _controller.UpdateRole(1, CreateUpdateRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task UpdateRole_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.UpdateRole(1, CreateUpdateRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task UpdateRole_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.UpdateRoleAsync(1, It.IsAny<RoleUpdateRequest>()))
                        .ReturnsAsync(CreateRoleResponse());

            await _controller.UpdateRole(1, CreateUpdateRequest());

            _serviceMock.Verify(s => s.UpdateRoleAsync(1, It.IsAny<RoleUpdateRequest>()), Times.Once);
        }

        #endregion

        #region DeleteRole

        [Test]
        public async Task DeleteRole_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.DeleteRoleAsync(1))
                        .Returns(Task.CompletedTask);

            var result = await _controller.DeleteRole(1);

            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public async Task DeleteRole_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.DeleteRoleAsync(1))
                        .Returns(Task.CompletedTask);

            await _controller.DeleteRole(1);

            _serviceMock.Verify(s => s.DeleteRoleAsync(1), Times.Once);
        }

        #endregion

        #region PatchRoleStatus

        [Test]
        public async Task PatchRoleStatus_ValidModel_ReturnsOk()
        {
            _serviceMock.Setup(s => s.PatchRoleStatusAsync(1, It.IsAny<RoleStatusPatchRequest>()))
                        .ReturnsAsync(CreateRoleResponse());

            var result = await _controller.PatchRoleStatus(1, CreatePatchRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task PatchRoleStatus_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "invalid");

            var result = await _controller.PatchRoleStatus(1, CreatePatchRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task PatchRoleStatus_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.PatchRoleStatusAsync(1, It.IsAny<RoleStatusPatchRequest>()))
                        .ReturnsAsync(CreateRoleResponse());

            await _controller.PatchRoleStatus(1, CreatePatchRequest());

            _serviceMock.Verify(s => s.PatchRoleStatusAsync(1, It.IsAny<RoleStatusPatchRequest>()), Times.Once);
        }

        [Test]
        public async Task PatchRoleStatus_NullRequest_ReturnsOk()
        {
            var result = await _controller.PatchRoleStatus(1, null);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        #endregion
    }
}