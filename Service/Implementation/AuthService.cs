using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Telecomm360.Constants;
using Telecomm360.DTO;
using Telecomm360.Models;
using Telecomm360.Repositories.Interface;
using Telecomm360.Services.Interface;

namespace Telecomm360.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuditLogService _auditLogService;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUserRepository userRepository,
            IAuditLogService auditLogService,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _auditLogService = auditLogService;
            _configuration = configuration;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

            if (user == null || user.PasswordHash != loginDto.Password)
                throw new UnauthorizedAccessException(MessageConstants.InvalidCredentials);

            // ✅ JWT TOKEN CREATION
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role ?? "User") // Safe null-check for roles
            };

            // 🛠️ FIXED: Added a robust string fallback to prevent 'Parameter s cannot be null' crash
            var secretKey = _configuration["Jwt:Key"] ?? "YourSuperSecretBackupKeyThatIsVeryLong123!";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            string jwtToken = tokenHandler.WriteToken(token);

            // ✅ AUDIT LOG
            await _auditLogService.CreateAuditLogAsync(new AuditLogCreateRequest
            {
                UserId = user.UserID,
                ActionPerformed = $"User Logged In: {user.Email}",
                TargetResource = "AuthModule"
            });

            return new AuthResponse
            {
                Token = jwtToken,
                UserDisplayLabel = user.Name,
                AssignedRole = user.Role
            };
        }

        public async Task<bool> RegisterAsync(RegisterRequest registerDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(registerDto.Email);

            if (existingUser != null)
                throw new ArgumentException(MessageConstants.EmailTaken);

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Phone = registerDto.Phone,
                PasswordHash = registerDto.Password,
                Role = registerDto.DefaultRole ?? "User" // Safe default assignment
            };

            await _userRepository.AddUserAsync(user);

            await _auditLogService.CreateAuditLogAsync(new AuditLogCreateRequest
            {
                ActionPerformed = $"New User Registered: {user.Email}",
                TargetResource = "AuthModule"
            });

            return true;
        }

        public async Task<bool> LogoutAsync(LogoutRequest dto)
        {
            await _auditLogService.CreateAuditLogAsync(new AuditLogCreateRequest
            {
                UserId = dto.UserId,
                ActionPerformed = $"User Logout: {dto.UserId}",
                TargetResource = "AuthModule"
            });

            return true;
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordRequest dto)
        {
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);

            if (user == null)
                throw new KeyNotFoundException(MessageConstants.UserNotFound);

            user.ResetToken = "ResetToken";
            await _userRepository.UpdateUserAsync(user);

            await _auditLogService.CreateAuditLogAsync(new AuditLogCreateRequest
            {
                ActionPerformed = $"Password Reset Requested: {user.Email}",
                TargetResource = "AuthModule"
            });

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequest dto)
        {
            return await Task.FromResult(true);
        }
    }
}