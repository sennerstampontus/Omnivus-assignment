using Microsoft.AspNetCore.Mvc;

namespace OmnivusMvcWebsite.Controllers
{
    public class NotFoundController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ProfileError()
        {
            return View();
        }
    }
}
