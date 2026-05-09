using CRM.WebApp.DTOs.Register;
using Microsoft.AspNetCore.Identity;

namespace CRM.WebApp.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto);
        Task<SignInResult> LoginUserAsync(LoginDto loginDto);
        Task LogoutUserAsync();
        Task<ApplicationUserDto> GetUserByEmailAsync(string email);
        Task<List<ApplicationUserDto>> GetAllUserAsync(); 
    }
}
