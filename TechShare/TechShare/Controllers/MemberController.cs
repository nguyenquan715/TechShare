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
using Microsoft.EntityFrameworkCore.Internal;

namespace TechShare.Controllers
{
    [Authorize(Roles =RoleConstant.Member)]
    public class MemberController : Controller
    {
        private IPostService _postService;
        public MemberController(IPostService postService)
        {
            _postService = postService;
        }
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
        
        //public IActionResult EditPost(Guid postId)
        //{
        //    try
        //    {
        //        var result = _postService.GetPostOfUser(postId);
        //        dynamic post = result.ElementAt(0);
        //        PostViewModel model = new PostViewModel()
        //        {
        //            Id = post["Id"],
        //            Title = post["Title"],
        //            Content = post["Content"],
        //            CreatedAt = post["CreatedAt"],
        //            UpdatedAt = post["UpdatedAt"],
        //            SubmitedAt = post["SubmitedAt"],
        //            PublishedAt = post["PublishedAt"],
        //            Status = post["Status"],
        //            UserId = post["UserId"],
        //            Avatar = post["Avatar"],
        //            CategoriesId = FormatDataRows(post["CategoriesId"], "<Id>", "</Id>"),
        //            CategoriesName = FormatDataRows(post["CategoriesName"], "<Name>", "</Name>")
        //        };
        //        //Kiểm tra xem bài viết có phải của user không
        //        if (model.UserId != HttpContext.Session.GetString("userId"))
        //        {
        //            return RedirectToAction("AccessDenied", "Account");
        //        }
        //        return View(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("Error", "Home");
        //    }            
        //}        
    }
}