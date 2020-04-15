using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechShare.Models;

namespace TechShare.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        /*Hiển thị trang đăng nhập*/
        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }

        /*Hiển thị trang đăng ký*/
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        /*Đăng ký tài khoản*/
        [HttpPost]
        public async Task<IActionResult> Signup([FromForm]SignupModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Password not match!");
                }
                else
                {
                    AppUser user = new AppUser() {
                        UserName = model.UserName,
                        FirstName=model.FirstName,
                        LastName=model.LastName,
                        Email=model.Email,
                        Job=model.Job,
                        Gender=model.Gender
                    };                   
                    IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (IdentityError err in result.Errors)
                        {
                            ModelState.AddModelError("", err.Description);
                        }
                    }
                }
            }
            return View(model);
        }
    }
}