using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Telecomm360.Constants;
using Telecomm360.DTO;
using Telecomm360.Model;
using Telecomm360.Repositories.Interface;
using Telecomm360.Service.Interface;
 
namespace Telecomm360.Service.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuditLogService _auditLogService;
        private readonly IConfiguration _configuration;
        private readonly IRoleRepository _roleRepository;
 
        public AuthService(
            IUserRepository userRepository,
            IAuditLogService auditLogService,
            IConfiguration configuration,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _auditLogService = auditLogService;
            _configuration = configuration;
            _roleRepository = roleRepository;
        }
 
        // ✅ LOGIN
        public async Task<AuthResponse> LoginAsync(LoginRequest loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
 
            if (user == null || user.PasswordHash != loginDto.Password)
                throw new UnauthorizedAccessException(MessageConstants.InvalidCredentials);
 
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleEntity?.Name ?? "User")
            };
 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"] ?? "YourSuperSecretBackupKeyThatIsVeryLong123!"
            ));
 
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
 
            // ✅ Audit log
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
                AssignedRole = user.RoleEntity?.Name ?? "User"
            };
        }
 
        // ✅ REGISTER (FULL FIX)
        public async Task<bool> RegisterAsync(RegisterRequest registerDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(registerDto.Email);
 
            if (existingUser != null)
                throw new ArgumentException(MessageConstants.EmailTaken);
 
            // ✅ Get role from DB
            var role = await _roleRepository.GetRoleByNameAsync(registerDto.DefaultRole ?? "User");
 
            if (role == null)
                throw new Exception("Role not found. Please insert roles in DB.");
 
            // ✅ Create user
            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Phone = registerDto.Phone,
                PasswordHash = registerDto.Password,
                RoleID = role.RoleID,
                RoleEntity = role
            };
 
            // ✅ Save user
            await _userRepository.AddUserAsync(user);
 
            // ✅ Audit log
            await _auditLogService.CreateAuditLogAsync(new AuditLogCreateRequest
            {
                UserId = user.UserID,
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
                return false;
 
            user.ResetToken = Guid.NewGuid().ToString();
 
            await _userRepository.UpdateUserAsync(user);
 
            await _auditLogService.CreateAuditLogAsync(new AuditLogCreateRequest
            {
                UserId = user.UserID,
                ActionPerformed = $"Password Reset Requested: {user.Email}",
                TargetResource = "AuthModule"
            });
 
            return true;
        }
 
        public async Task<bool> ResetPasswordAsync(ResetPasswordRequest dto)
        {
            var user = await _userRepository.GetUserByResetTokenAsync(dto.Token);

            if (user == null)
                return false;

            user.PasswordHash = dto.NewPassword;
            user.ResetToken = string.Empty;

            await _userRepository.UpdateUserAsync(user);
            return true;
        }
    }
}