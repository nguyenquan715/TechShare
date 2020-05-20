using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechShare.Infra;
using TechShare.Models;
using TechShare.Entity;

namespace TechShare.Controllers
{
    [Authorize(Roles =RoleConstant.Employee)]
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userData")))
            {
                AppUser user = JsonSerializer.Deserialize<AppUser>(HttpContext.Session.GetString("userData"));
                return View(user);
            }
            return RedirectToAction("Signout", "Account");
        }
    }
}