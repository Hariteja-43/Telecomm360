using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telecomm360.Constants;
using Telecomm360.DTO;
using Telecomm360.Service.Interface;
 
namespace Telecomm360.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
 
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
 
        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(MessageConstants.InvalidModel);
            }
 
            var response = await _authService.LoginAsync(loginDto);
            return Ok(response);
        }
 
        // LOGOUT
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest logoutDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(MessageConstants.InvalidModel);
            }
 
            var response = await _authService.LogoutAsync(logoutDto);
 
            if (!response)
            {
                return BadRequest(new { Message = "Logout failed. Please try again." });
            }
 
            return Ok(new { Message = "Logout successful." });
        }
 
        // REGISTER (FIXED)
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(MessageConstants.InvalidModel);
            }
 
            var response = await _authService.RegisterAsync(registerDto);
 
            return Ok(new { Message = "User Registered Successfully", Status = response });
        }
 
        // FORGOT PASSWORD
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest forgotPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(MessageConstants.InvalidModel);
            }
 
            var response = await _authService.ForgotPasswordAsync(forgotPasswordDto);
            return Ok(response);
        }
 
        // RESET PASSWORD
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(MessageConstants.InvalidModel);
            }
 
            var response = await _authService.ResetPasswordAsync(resetPasswordDto);
            return Ok(response);
        }
    }
}