using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OmnivusMvcWebsite.Data;
using OmnivusMvcWebsite.Models;
using OmnivusMvcWebsite.Models.ViewModels;

namespace OmnivusMvcWebsite.Services
{
    public interface IRolesManager
    {
        public Task<AdminCreateRoleViewModel> GetCreateRoleView(string userId);
        public Task<AdminRolesViewModel> GetAllRolesAsync(string userId);
        public Task<string> CreateRoleAsync(string roleName);
        public Task<IdentityRole> UpdateRoleAsync(string roleId, string roleName);
    }
    public class RolesManager : IRolesManager
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public RolesManager(RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<string> CreateRoleAsync(string roleName)
        {
            try
            {             

                var newRole = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (newRole.Succeeded)
                {
                    string role = roleName;

                    return role;
                }

                return null;


            } catch { return null; }
            
        }

        public async Task<AdminRolesViewModel> GetAllRolesAsync(string userId)
        {
            var roles = new List<string>();
            foreach (var role in _roleManager.Roles)
            {
                roles.Add(role.Name);
            
            }
            var profileEntity = await _context.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userId);
            var userProfile = new UserProfile
            {
                FirstName = profileEntity.FirstName,
                LastName = profileEntity.LastName
            };

            var sortedRoleList = roles.OrderBy(x => x).ToList();

            var adminRoles = new AdminRolesViewModel
            {
                RoleNames = sortedRoleList,
                UserProfile = userProfile
            };

            return adminRoles;

        }

        public async Task<AdminCreateRoleViewModel> GetCreateRoleView(string userId)
        {
            var adminModel = await GetAllRolesAsync(userId);
            string newRoleName = "";
            string oldRoleName = "";

            var viewModel = new AdminCreateRoleViewModel
            {
                AdminRoles = adminModel,
                NewRoleName = newRoleName,
                OldRoleName = oldRoleName
                
            };

            return viewModel;
        }

        public async Task<IdentityRole> UpdateRoleAsync(string oldRoleName, string roleName)
        {
            var oldRole = await _roleManager.FindByNameAsync(oldRoleName);
            var newRole = new IdentityRole()
            {
                Name = roleName
            };

            if (oldRole != null)
            {
                await _roleManager.DeleteAsync(oldRole);
                await _roleManager.CreateAsync(newRole);
            }

            return newRole;
        }
    }
}
