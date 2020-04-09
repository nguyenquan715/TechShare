using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TechShare.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Member()
        {
            return View();
        }
        public IActionResult Employee()
        {
            return View();
        }
        public IActionResult Administrator()
        {
            return View();
        }
    }
}