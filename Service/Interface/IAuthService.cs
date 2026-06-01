using System.Threading.Tasks;
using Telecomm360.DTO;

namespace Telecomm360.Services.Interface
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest loginDto);
        Task<bool> RegisterAsync(RegisterRequest registerDto);
        Task<bool> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordDto);
        Task<bool> ResetPasswordAsync(ResetPasswordRequest resetPasswordDto);
        Task<bool> LogoutAsync(LogoutRequest logoutDto);
        
    }
}