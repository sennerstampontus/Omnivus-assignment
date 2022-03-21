using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OmnivusMvcWebsite.Data;
using OmnivusMvcWebsite.Models;

namespace OmnivusMvcWebsite.Controllers
{
    public class ProfileEntityController : Controller
    {
        private readonly AppDbContext _context;

        public ProfileEntityController(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Profiles.Include(x => x.User);
            return View(await appDbContext.ToListAsync());
        }


        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
                return NotFound();

            var profileEntity = await _context.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (profileEntity == null)
                return NotFound();

            return View(profileEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, FirstName, LastName, Email, StreetName, PostalCode, City, Country, UserId")] ProfileEntity profileEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profileEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", profileEntity.UserId);
            return View(profileEntity);
        }
    }
}
