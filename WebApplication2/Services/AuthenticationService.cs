using AutoMapper;
using CRM.Domain.IdentityEntity;
using CRM.WebApp.DTOs.Register;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.Security;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly ISecureConfigurationService _secureConfigurationService;
        private readonly IInputValidationService _inputValidationService;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            ILogger<AuthenticationService> logger,
            ISecureConfigurationService secureConfigurationService,
            IInputValidationService inputValidationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
            _secureConfigurationService = secureConfigurationService;
            _inputValidationService = inputValidationService;
        }


        public async Task<ApplicationUserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return _mapper.Map<ApplicationUserDto>(user);
        }

        public async Task<SignInResult> LoginUserAsync(LoginDto loginDto)
        {
            // Input validation
            if (!_inputValidationService.IsValidEmail(loginDto.Email))
            {
                _logger.LogWarning("Invalid email format attempted for login: {Email}", loginDto.Email);
                return SignInResult.Failed;
            }

            if (!_inputValidationService.IsSafeInput(loginDto.Email) || !_inputValidationService.IsSafeInput(loginDto.Password))
            {
                _logger.LogWarning("Potentially malicious input detected in login attempt for email: {Email}", loginDto.Email);
                return SignInResult.Failed;
            }

            try
            {
                // Enable account lockout for security
                var result = await _signInManager.PasswordSignInAsync(
                    loginDto.Email,
                    loginDto.Password,
                    loginDto.RememberMe,
                    lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(loginDto.Email);
                    _logger.LogInformation("Successful login for user: {UserId} at {Timestamp}", user.Id, DateTime.UtcNow);
                    
                    // Reset failed attempts on successful login
                    await _userManager.ResetAccessFailedCountAsync(user);
                    
                    // Add security claims
                    await AddSecurityClaimsAsync(user);
                }
                else if (result.IsLockedOut)
                {
                    _logger.LogWarning("Account locked out for user: {Email} at {Timestamp}", loginDto.Email, DateTime.UtcNow);
                }
                else if (result.IsNotAllowed)
                {
                    _logger.LogWarning("Login not allowed for user: {Email} at {Timestamp}", loginDto.Email, DateTime.UtcNow);
                }
                else
                {
                    _logger.LogWarning("Failed login attempt for email: {Email} at {Timestamp}", loginDto.Email, DateTime.UtcNow);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login attempt for email: {Email}", loginDto.Email);
                return SignInResult.Failed;
            }
        }

        private async Task AddSecurityClaimsAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("last_login", DateTime.UtcNow.ToString("O")),
                new Claim("session_id", _secureConfigurationService.GenerateSecureToken())
            };

            await _userManager.AddClaimsAsync(user, claims);
        }

        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerDto);
            user.UserName = registerDto.Email; // Typically, email is used as username

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            // If you had custom user data to save via your repository, you'd do it here
            // if (result.Succeeded)
            // {
            //     await _unitOfWork.Users.AddAsync(user);
            //     await _unitOfWork.CompleteAsync();
            // }

            return result;
        }

        public async Task<List<ApplicationUserDto>> GetAllUserAsync()
        {
            var user = await _userManager.Users.AsNoTracking()
                .Select(a=> new ApplicationUser 
                { 
                    NameAR = a.NameAR,
                    Id = a.Id,
                    Email = a.Email
                }).ToListAsync();
            return _mapper.Map<List<ApplicationUserDto>>(user);
        }
    }
}
