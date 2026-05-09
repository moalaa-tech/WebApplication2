using CRM.Domain.IdentityEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.WebApp.Controllers
{
    public class RoleController : Controller
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

                return View(roles);
            }
            catch (Exception ex)
            {
                return View();
            }

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationRole applicationRole)
        {
            if (!ModelState.IsValid)
            {
                return View(applicationRole);
            }


            var role = new ApplicationRole { Name = applicationRole.Name, NameAR = applicationRole.NameAR };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View();
        }


        // Assumes this code is inside a controller, e.g., RoleController

        // GET: /Role/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

          
            var model = new ApplicationRole
            {
                Id = role.Id,
                Name = role.Name,
                NameAR = role.NameAR
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationRole applicationRole)
        {
            // 1. Validate the incoming model data
            if (!ModelState.IsValid)
            {
                return View(applicationRole);
            }

            // 2. Retrieve the existing role object from the database using its Id
            var role = await _roleManager.FindByIdAsync(applicationRole.Id.ToString());

            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Role not found.");
                return View(applicationRole);
            }

            role.Name = applicationRole.Name;
            role.NameAR = applicationRole.NameAR;

            // 4. Update the role in the database
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            // 5. If the update failed (e.g., duplicate role name), add errors to ModelState
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(applicationRole);
        }

    }
}
