using Microsoft.AspNetCore.Authorization;
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
        private readonly IWebHostEnvironment _host;

        public ProfileController(IProfileManager profileManager, AppDbContext context, IWebHostEnvironment host)
        {
            _profileManager = profileManager;
            _context = context;
            _host = host;
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Index(string id)
        {
            var profile = await _profileManager.ReadAsync(id);
            return View(profile);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var profile = await _profileManager.ReadAsync(id);
            return View(profile);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(string id, UserProfile model)
        {
            try
            {
                var profileEntity = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == id);

                if (model.File != null)
                {
                    string wwwrootPath = _host.WebRootPath;
                    string fileName = $"{Path.GetFileNameWithoutExtension("Profile")}_{profileEntity.UserId}{Path.GetExtension(model.File.FileName)}";
                    string filePath = Path.Combine($"{wwwrootPath}/img", fileName);

                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(fs);
                    }

                    model.FileName = fileName;

                    if (profileEntity != null)
                    {
                        profileEntity.FirstName = model.FirstName;
                        profileEntity.LastName = model.LastName;
                        profileEntity.Email = model.Email;
                        profileEntity.StreetName = model.StreetName;
                        profileEntity.PostalCode = model.PostalCode;
                        profileEntity.City = model.City;
                        profileEntity.Country = model.Country;
                        profileEntity.Bio = model.Bio;
                        profileEntity.FileName = fileName;
                        if (model.Bio == null)
                        {
                            profileEntity.Bio = "";
                        }
                    }

                }

                else
                {
                    if (profileEntity != null)
                    {
                        profileEntity.FirstName = model.FirstName;
                        profileEntity.LastName = model.LastName;
                        profileEntity.Email = model.Email;
                        profileEntity.StreetName = model.StreetName;
                        profileEntity.PostalCode = model.PostalCode;
                        profileEntity.City = model.City;
                        profileEntity.Country = model.Country;
                        profileEntity.Bio = model.Bio;
                        profileEntity.FileName = profileEntity.FileName;
                        if (model.Bio == null)
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
