using CRM.Domain.IdentityEntity;
using CRM.WebApp.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("ManageRoles")]
        public async Task<IActionResult> ManageRoles([FromQuery] string? userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new ManageUserRolesViewModel
            {
                UserId = user.Id.ToString(),
                UserName = user.UserName,
                Roles = roles.Select(r => new SelectRoleViewModel
                {
                    RoleName = r.Name,
                    Selected = userRoles.Contains(r.Name)
                }).ToList()
            };

            return Ok(model);
        }

        [HttpPost("ManageRoles")]
        public async Task<IActionResult> ManageRoles([FromBody] ManageUserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = model.Roles.Where(r => r.Selected).Select(r => r.RoleName);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
            if (!result.Succeeded)
            {
                // Handle error
                return Ok(model);
            }

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
            if (!result.Succeeded)
            {
                // Handle error
                return Ok(model);
            }

            return Ok();
        }
    }
}