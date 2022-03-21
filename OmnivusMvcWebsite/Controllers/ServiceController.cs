using Microsoft.AspNetCore.Mvc;

namespace OmnivusMvcWebsite.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
