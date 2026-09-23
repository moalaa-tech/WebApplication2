using CRM.Domain.IdentityEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var roles = await _roleManager.Roles.ToListAsync();

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return Ok();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ApplicationRole applicationRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var role = new ApplicationRole { Name = applicationRole.Name, NameAR = applicationRole.NameAR };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok();
            }

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ApplicationRole applicationRole)
        {
            // 1. Validate the incoming model data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // 2. Retrieve the existing role object from the database using its Id
            var role = await _roleManager.FindByIdAsync(applicationRole.Id.ToString());

            if (role == null)
            {
                return BadRequest(new { message = "Role not found." });
            }

            role.Name = applicationRole.Name;
            role.NameAR = applicationRole.NameAR;

            // 4. Update the role in the database
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return Ok();
            }

            // 5. If the update failed (e.g., duplicate role name), add errors to ModelState
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }
    }
}