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

        public IActionResult Index()
        {
            try
            {                               
                var list = _postService.GetListPostsPublished(1, 5);
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
            return View();
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
