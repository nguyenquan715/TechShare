using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechShare.Models;
using TechShare.Entity;
using TechShare.Service;
using System.Dynamic;
using Newtonsoft.Json;
using TechShare.Infra;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechShare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IPostService _postService;
        private static Utility _utility=new Utility();

        public HomeController(ILogger<HomeController> logger,IPostService postService)
        {
            _logger = logger;
            _postService = postService;            
        }

        public IActionResult Index(int page=1)
        {
            try
            {
                int begin = (page-1) * 6 + 1;
                int end = begin + 5;
                var list = _postService.GetListPostsPublished(begin,end);
                string json=JsonConvert.SerializeObject(list);
                dynamic temp = JsonConvert.DeserializeObject(json);
                dynamic res = temp[0];
                List<PostViewModel> model = new List<PostViewModel>();
                for(int i = 0; i < res.Count; i++)
                {                                       
                    model.Add(_utility.BindingDataIntoPostModel(res[i]));
                }
                return View(model);
            }catch(Exception ex)
            {
                return RedirectToAction("Error");
            }            
        }        

        /*Hiển thị một bài viết*/
        public IActionResult Post(Guid id)
        {
            try
            {
                var post = _postService.GetPostOfUser(id);
                string json = JsonConvert.SerializeObject(post);
                dynamic temp = JsonConvert.DeserializeObject(json);
                dynamic res = temp[0];
                PostViewModel model = _utility.BindingDataIntoPostModel(res[0]);
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }            
        }

        /*Hiển thị kết quả tìm kiếm*/
        public IActionResult Search(string keyword)
        {
            try
            {
                var list = _postService.GetPublishedPostsByTitle(keyword);
                string json = JsonConvert.SerializeObject(list);
                dynamic temp = JsonConvert.DeserializeObject(json);
                dynamic res = temp[0];
                List<PostViewModel> model = new List<PostViewModel>();
                for (int i = 0; i < res.Count; i++)
                {
                    model.Add(_utility.BindingDataIntoPostModel(res[i]));
                }
                return View(model);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        /*Hiển thị các bài viết của một tác giả*/
        public IActionResult PostsOfAuthor(string userId)
        {
            try
            {
                var list = _postService.GetPostsOfAuthor(userId);
                string json = JsonConvert.SerializeObject(list);
                dynamic temp = JsonConvert.DeserializeObject(json);
                dynamic res = temp[0];
                List<PostViewModel> model = new List<PostViewModel>();
                for (int i = 0; i < res.Count; i++)
                {
                    model.Add(_utility.BindingDataIntoPostModel(res[i]));
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }               
    }
}
