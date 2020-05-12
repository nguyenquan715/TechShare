using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechShare.Models;

namespace TechShare.Controllers
{
    [Authorize(Roles ="member")]
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            AppUser user = JsonSerializer.Deserialize<AppUser>(HttpContext.Session.GetString("userData"));
            return View(user);
        }
    }
}