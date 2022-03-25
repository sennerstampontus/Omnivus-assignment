using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmnivusMvcWebsite.Data;
using OmnivusMvcWebsite.Models;
using OmnivusMvcWebsite.Models.ViewModels;
using OmnivusMvcWebsite.Services;

namespace OmnivusMvcWebsite.Controllers
{
    [Authorize]
    [Route("/profile")]
    public class ProfileController : Controller
    {

        private readonly IProfileManager _profileManager;
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _host;

        public ProfileController(IProfileManager profileManager, AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment host)
        {
            _profileManager = profileManager;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _host = host;
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Index(string id)
        {
            if (await _context.Users.FindAsync(id) != null)
            {
                var profile = await _profileManager.ReadAsync(id);
                return View(profile);
            }
            
            
           return RedirectToAction("Index", "NotFound");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var profile = await _profileManager.ReadAsync(id);
            return View(profile);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(string id, ProfileViewModel model)
        {
            try
            {
                var profileEntity = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == id);
                var userEntity = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == profileEntity.UserId);
                var userRoles = await _userManager.GetRolesAsync(userEntity);
                


                if(await _roleManager.FindByNameAsync(model.UserEntity.RoleName) != null)
                {
                    if (!await _userManager.IsInRoleAsync(userEntity, model.UserEntity.RoleName))
                    {
                        await _userManager.AddToRoleAsync(userEntity, model.UserEntity.RoleName);
                        foreach(var role in userRoles)
                        {
                            if(role != model.UserEntity.RoleName)
                            {
                                await _userManager.RemoveFromRoleAsync(userEntity, role);
                            }
                        }
                        _context.Entry(userEntity).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }

                else
                {
                    foreach (var role in userRoles)
                    {
                        await _userManager.RemoveFromRoleAsync(userEntity, role);
                    }
                    await _roleManager.CreateAsync(new IdentityRole(model.UserEntity.RoleName));
                    await _userManager.AddToRoleAsync(userEntity, model.UserEntity.RoleName);

                    _context.Entry(userEntity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                

                if (model.UserProfile.File != null)
                {
                    string wwwrootPath = _host.WebRootPath;
                    string fileName = $"{Path.GetFileNameWithoutExtension("Profile")}_{profileEntity.UserId}{Path.GetExtension(model.UserProfile.File.FileName)}";
                    string filePath = Path.Combine($"{wwwrootPath}/img", fileName);

                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await model.UserProfile.File.CopyToAsync(fs);
                    }

                    model.UserProfile.FileName = fileName;

                    if (profileEntity != null)
                    {
                        profileEntity.FirstName = model.UserProfile.FirstName;
                        profileEntity.LastName = model.UserProfile.LastName;
                        
                        profileEntity.Email = model.UserProfile.Email;
                        profileEntity.StreetName = model.UserProfile.StreetName;
                        profileEntity.PostalCode = model.UserProfile.PostalCode;
                        profileEntity.City = model.UserProfile.City;
                        profileEntity.Country = model.UserProfile.Country;
                        profileEntity.Bio = model.UserProfile.Bio;
                        profileEntity.FileName = fileName;
                        if (model.UserProfile.Bio == null)
                        {
                            profileEntity.Bio = "";
                        }
                    }

                }

                else
                {
                    if (profileEntity != null)
                    {
                        profileEntity.FirstName = model.UserProfile.FirstName;
                        profileEntity.LastName = model.UserProfile.LastName;
                        profileEntity.Email = model.UserProfile.Email;
                        profileEntity.StreetName = model.UserProfile.StreetName;
                        profileEntity.PostalCode = model.UserProfile.PostalCode;
                        profileEntity.City = model.UserProfile.City;
                        profileEntity.Country = model.UserProfile.Country;
                        profileEntity.Bio = model.UserProfile.Bio;
                        profileEntity.FileName = profileEntity.FileName;
                        if (model.UserProfile.Bio == null)
                        {
                            profileEntity.Bio = "";
                        }
                    }
                }

                _context.Entry(profileEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var profile = await _profileManager.ReadAsync(id);

                return RedirectToAction("Index", "Profile", new { Id = id });
            }

            catch { return View(model); }

        
        }
    }
}
