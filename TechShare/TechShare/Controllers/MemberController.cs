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
using TechShare.Service;

namespace TechShare.Controllers
{
    [Authorize(Roles =RoleConstant.Member)]
    public class MemberController : Controller
    {        
        public IActionResult Index()
        {
            //Nếu tồn tại phiên
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userData")))
            {
                AppUser user = JsonSerializer.Deserialize<AppUser>(HttpContext.Session.GetString("userData"));
                //Nếu member không bị chặn
                if (!user.Blocked)
                {
                    return View(user);
                }
                return RedirectToAction("AccessDenied", "Account");
            }
            return RedirectToAction("Signout", "Account");  
        }        
    }
}