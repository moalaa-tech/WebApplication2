using AutoMapper;
using CRM.WebApp.DTOs.Register;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _accountService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var registerDto = _mapper.Map<RegisterDto>(model);
            var result = await _accountService.RegisterUserAsync(registerDto);

            if (result.Succeeded)
            {
                return Ok(new { succeeded = true });
            }

            return BadRequest(new
            {
                succeeded = false,
                errors = result.Errors.Select(e => e.Description)
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var loginDto = _mapper.Map<LoginDto>(model);
            var result = await _accountService.LoginUserAsync(loginDto);

            if (result.Succeeded)
            {
                return Ok(new { succeeded = true });
            }

            if (result.IsLockedOut)
            {
                return BadRequest(new { succeeded = false, message = "User account locked out." });
            }

            return Unauthorized(new { succeeded = false, message = "Invalid login attempt." });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutUserAsync();
            return Ok(new { succeeded = true });
        }
    }
}