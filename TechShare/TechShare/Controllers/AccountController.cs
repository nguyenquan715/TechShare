using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechShare.Models;

namespace TechShare.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /*Hiển thị trang đăng nhập*/
        [HttpGet]
        public IActionResult Signin()
        {            
            return View();
        }

        /*Đăng nhập*/
        [HttpPost]
        public async Task<IActionResult> Signin(SigninModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password,true, false);
                    //Nếu đăng nhập thành công
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("userData", JsonSerializer.Serialize<AppUser>(user));
                        //User có role là admin
                        if (await _userManager.IsInRoleAsync(user, "admin"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        //User có role là employee
                        if(await _userManager.IsInRoleAsync(user, "employee"))
                        {
                            return RedirectToAction("Index", "Employee");
                        }
                        //User có role là member
                        if(await _userManager.IsInRoleAsync(user, "member"))
                        {
                            return RedirectToAction("Index", "Member");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                //Nếu đăng nhập thất bại
                ModelState.AddModelError("", "Email hoặc mật khẩu không chính xác!");
            }
            return View(model);
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
                    //Tạo thành công
                    if (result.Succeeded)
                    {
                        AppUser u = await _userManager.FindByEmailAsync(user.Email);
                        var res = await _userManager.AddToRoleAsync(u, "member");
                        if (res.Succeeded)
                        {
                            return RedirectToAction("Login");
                        }
                        foreach (IdentityError err in res.Errors)
                        {
                            ModelState.AddModelError("", err.Description);
                        }
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

        /*Đăng xuất*/
        [Authorize]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        /*Access Denied*/
        public IActionResult AccessDenied()
        {
            return View();
        }

        /*Quên mật khẩu*/
    }
}