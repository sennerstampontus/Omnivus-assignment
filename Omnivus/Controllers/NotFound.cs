using Microsoft.AspNetCore.Mvc;

namespace Omnivus.Controllers
{
    public class NotFound : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
