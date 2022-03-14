using Microsoft.AspNetCore.Mvc;

namespace Omnivus.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

