﻿using Microsoft.AspNetCore.Mvc;

namespace Omnivus.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
