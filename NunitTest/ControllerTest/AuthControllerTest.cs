using NUnit.Framework;
using Moq;
using Telecomm360.Controllers;
using Telecomm360.Service.Interface;
using Telecomm360.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IAuthService> _serviceMock;
        private AuthController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IAuthService>();
            _controller = new AuthController(_serviceMock.Object);
        }

        // Request helpers

        private LoginRequest GetLoginRequest() => new LoginRequest
        {
            Email = "user@test.com",
            Password = "password123"
        };

        private LogoutRequest GetLogoutRequest() => new LogoutRequest
        {
            UserId = 1
        };

        private RegisterRequest GetRegisterRequest() => new RegisterRequest
        {
            Name = "Test",
            Email = "user@test.com",
            Phone = "1234567890",
            Password = "password123",
            DefaultRole = "User"
        };

        private ForgotPasswordRequest GetForgotRequest() => new ForgotPasswordRequest
        {
            Email = "user@test.com"
        };

        private ResetPasswordRequest GetResetRequest() => new ResetPasswordRequest
        {
            Token = "token123",
            NewPassword = "newPassword123"
        };

        private AuthResponse GetAuthResponse() => new AuthResponse
        {
            Token = "jwt",
            UserDisplayLabel = "Test",
            AssignedRole = "User"
        };

        // ---------------- LOGIN ----------------

        [Test]
        public async Task Login_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.LoginAsync(It.IsAny<LoginRequest>()))
                        .ReturnsAsync(GetAuthResponse());

            var result = await _controller.Login(GetLoginRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task Login_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("x", "invalid");

            var result = await _controller.Login(GetLoginRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task Login_CallsService()
        {
            _serviceMock.Setup(s => s.LoginAsync(It.IsAny<LoginRequest>()))
                        .ReturnsAsync(GetAuthResponse());

            await _controller.Login(GetLoginRequest());

            _serviceMock.Verify(s => s.LoginAsync(It.IsAny<LoginRequest>()), Times.Once);
        }

        // ---------------- LOGOUT ----------------

        [Test]
        public async Task Logout_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.LogoutAsync(It.IsAny<LogoutRequest>()))
                        .ReturnsAsync(true);

            var result = await _controller.Logout(GetLogoutRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task Logout_Failure_ReturnsBadRequest()
        {
            _serviceMock.Setup(s => s.LogoutAsync(It.IsAny<LogoutRequest>()))
                        .ReturnsAsync(false);

            var result = await _controller.Logout(GetLogoutRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task Logout_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("x", "invalid");

            var result = await _controller.Logout(GetLogoutRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        // ---------------- REGISTER ----------------

        [Test]
        public async Task Register_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.RegisterAsync(It.IsAny<RegisterRequest>()))
                        .ReturnsAsync(true);

            var result = await _controller.Register(GetRegisterRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task Register_Failure_ReturnsOkFalse()
        {
            _serviceMock.Setup(s => s.RegisterAsync(It.IsAny<RegisterRequest>()))
                        .ReturnsAsync(false);

            var result = await _controller.Register(GetRegisterRequest())
                                           as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(false));
        }

        [Test]
        public async Task Register_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("x", "invalid");

            var result = await _controller.Register(GetRegisterRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        // ---------------- FORGOT PASSWORD ----------------

        [Test]
        public async Task ForgotPassword_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.ForgotPasswordAsync(It.IsAny<ForgotPasswordRequest>()))
                        .ReturnsAsync(true);

            var result = await _controller.ForgotPassword(GetForgotRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task ForgotPassword_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("x", "invalid");

            var result = await _controller.ForgotPassword(GetForgotRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task ForgotPassword_ServiceCalled()
        {
            _serviceMock.Setup(s => s.ForgotPasswordAsync(It.IsAny<ForgotPasswordRequest>()))
                        .ReturnsAsync(true);

            await _controller.ForgotPassword(GetForgotRequest());

            _serviceMock.Verify(s => s.ForgotPasswordAsync(It.IsAny<ForgotPasswordRequest>()), Times.Once);
        }

        // ---------------- RESET PASSWORD ----------------

        [Test]
        public async Task ResetPassword_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.ResetPasswordAsync(It.IsAny<ResetPasswordRequest>()))
                        .ReturnsAsync(true);  

            var result = await _controller.ResetPassword(GetResetRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task ResetPassword_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("x", "invalid");

            var result = await _controller.ResetPassword(GetResetRequest());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task ResetPassword_ServiceCalled()
        {
            _serviceMock.Setup(s => s.ResetPasswordAsync(It.IsAny<ResetPasswordRequest>()))
                        .ReturnsAsync(true);  

            await _controller.ResetPassword(GetResetRequest());

            _serviceMock.Verify(s => s.ResetPasswordAsync(It.IsAny<ResetPasswordRequest>()), Times.Once);
        }
    }
}