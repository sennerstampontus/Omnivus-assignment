using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmnivusMvcWebsite.Data;
using OmnivusMvcWebsite.Models.ViewModels;
using OmnivusMvcWebsite.Services;

namespace OmnivusMvcWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("admin/")]
    public class AdminController : Controller
    {

        private readonly IProfileManager _profileManager;
        private readonly IRolesManager _rolesManager;
        private readonly RoleManager<IdentityRole> _identityRoleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _context;

        public AdminController(IProfileManager profileManager, IRolesManager rolesManager, RoleManager<IdentityRole> identityRoleManager, AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _profileManager = profileManager;
            _rolesManager = rolesManager;
            _identityRoleManager = identityRoleManager;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(string id)
        {
            if(await _context.Users.FindAsync(id) != null)
            {
                var adminProfile = await _profileManager.ReadAllAsync(id);
                return View(adminProfile);
            }
            return RedirectToAction("Index", "NotFound");
        }

        [HttpGet("{id}/roles")]
        public async Task<IActionResult> Roles(string id)
        {
            var adminRoles = await _rolesManager.GetCreateRoleView(id);
            return View(adminRoles);
        }

        [HttpGet("{id}/roles/create")]
        public async Task<IActionResult> CreateRole(string id)
        {
            var adminRoles = await _rolesManager.GetCreateRoleView(id);
            return View(adminRoles);
        }

        [HttpPost("{id}/roles/create")]
        public async Task<IActionResult> CreateRole(AdminCreateRoleViewModel model)
        {
            if (!string.IsNullOrEmpty(model.NewRoleName))
            {
                await _rolesManager.CreateRoleAsync(model.NewRoleName);
                return RedirectToAction("Roles", "Admin", new { Id = User.FindFirst("UserId").Value });
            }

            return RedirectToAction("CreateRole", "Admin", new { Id = User.FindFirst("UserId").Value });
        }

        [HttpPost("{id}/roles/edit")]
        public async Task<IActionResult> UpdateRole(AdminCreateRoleViewModel model)
        {
            if(model.NewRoleName != null)
            {
                await _rolesManager.UpdateRoleAsync(model.OldRoleName, model.NewRoleName);
            }
            return RedirectToAction("CreateRole", "Admin", new { Id = User.FindFirst("UserId").Value });
        }

        [HttpPost("{id}/roles/DeleteRole")]
        public async Task<IActionResult> DeleteRole(string oldRoleName)
        {
            if (oldRoleName != null)
            {
                var identityRole = await _identityRoleManager.FindByNameAsync(oldRoleName);
                

                await _identityRoleManager.DeleteAsync(identityRole);

                
            }
            return RedirectToAction("Roles", "Admin", new { Id = User.FindFirst("UserId").Value });
        }
    }
}
