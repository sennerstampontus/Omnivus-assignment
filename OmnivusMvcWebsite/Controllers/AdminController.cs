using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmnivusMvcWebsite.Data;
using OmnivusMvcWebsite.Services;

namespace OmnivusMvcWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("admin/")]
    public class AdminController : Controller
    {

        private readonly IProfileManager _profileManager;
        private readonly AppDbContext _context;

        public AdminController(IProfileManager profileManager, AppDbContext context)
        {
            _profileManager = profileManager;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var adminProfile = await _profileManager.ReadAllAsync(id);
            return View(adminProfile);
        }
    }
}
