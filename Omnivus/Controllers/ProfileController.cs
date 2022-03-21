using Microsoft.AspNetCore.Mvc;

namespace Omnivus.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
