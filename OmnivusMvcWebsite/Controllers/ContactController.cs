using Microsoft.AspNetCore.Mvc;

namespace OmnivusMvcWebsite.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
