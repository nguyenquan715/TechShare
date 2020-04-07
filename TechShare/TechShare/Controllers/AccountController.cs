using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TechShare.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View("Signin");
        }
        public IActionResult Signup()
        {
            return View();
        }
    }
}