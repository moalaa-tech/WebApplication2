using AutoMapper;
using CRM.WebApp.DTOs.Register;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _accountService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var registerDto = _mapper.Map<RegisterDto>(model);
                var result = await _accountService.RegisterUserAsync(registerDto);

                if (result.Succeeded)
                {
                    // Optionally, sign in the user immediately after registration
                    // await _accountService.LoginUserAsync(new LoginDto { Email = model.Email, Password = model.Password, RememberMe = false });

                    // Redirect to a confirmation page or home page
                    return RedirectToAction("RegisterConfirmation");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var loginDto = _mapper.Map<LoginDto>(model);
                var result = await _accountService.LoginUserAsync(loginDto);

                if (result.Succeeded)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home"); // Redirect to home page after successful login
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "User account locked out.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutUserAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult RegisterConfirmation()
        {
            return View();
        }
    }
}
