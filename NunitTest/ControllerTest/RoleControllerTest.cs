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

        private SearchDtos CreateSearchDto()
        {
            return new SearchDtos();
        }

        // FIXED: required properties added
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

        // FIXED: correct property name
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
            // Arrange
            var search = CreateSearchDto();
            var response = new List<RoleResponse>
            {
                CreateRoleResponse()
            };

            _serviceMock.Setup(s => s.GetRolesAsync(search))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.GetRoles(search);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task GetRoles_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "invalid");

            // Act
            var result = await _controller.GetRoles(CreateSearchDto());

            // Assert
            var badRequest = result as BadRequestObjectResult;
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.Value, Is.EqualTo(MessageConstants.InvalidModel));
        }

        #endregion

        #region CreateRole

        [Test]
        public async Task CreateRole_ValidModel_ReturnsOk()
        {
            // Arrange
            var request = CreateCreateRequest();
            var response = CreateRoleResponse();

            _serviceMock.Setup(s => s.CreateRoleAsync(request))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateRole(request);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task CreateRole_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "invalid");

            // Act
            var result = await _controller.CreateRole(CreateCreateRequest());

            // Assert
            var badRequest = result as BadRequestObjectResult;
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.Value, Is.EqualTo(MessageConstants.InvalidModel));
        }

        #endregion

        #region UpdateRole

        [Test]
        public async Task UpdateRole_ValidModel_ReturnsOk()
        {
            // Arrange
            var request = CreateUpdateRequest();
            var response = CreateRoleResponse();

            _serviceMock.Setup(s => s.UpdateRoleAsync(1, request))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateRole(1, request);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task UpdateRole_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "invalid");

            // Act
            var result = await _controller.UpdateRole(1, CreateUpdateRequest());

            // Assert
            var badRequest = result as BadRequestObjectResult;
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.Value, Is.EqualTo(MessageConstants.InvalidModel));
        }

        #endregion

        #region DeleteRole

        [Test]
        public async Task DeleteRole_Valid_ReturnsOk()
        {
            // Arrange
            _serviceMock.Setup(s => s.DeleteRoleAsync(1))
                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteRole(1);

            // Assert
            Assert.That(result, Is.TypeOf<OkResult>());
        }

        #endregion

        #region PatchRoleStatus

        [Test]
        public async Task PatchRoleStatus_ValidModel_ReturnsOk()
        {
            // Arrange
            var request = CreatePatchRequest();
            var response = CreateRoleResponse();

            _serviceMock.Setup(s => s.PatchRoleStatusAsync(1, request))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.PatchRoleStatus(1, request);

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task PatchRoleStatus_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "invalid");

            // Act
            var result = await _controller.PatchRoleStatus(1, CreatePatchRequest());

            // Assert
            var badRequest = result as BadRequestObjectResult;
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.Value, Is.EqualTo(MessageConstants.InvalidModel));
        }

        #endregion
    }
}