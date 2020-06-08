using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechShare.Infra;
using TechShare.Models;
using TechShare.Service;

namespace TechShare.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IPostService _postService;
        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        /*Lấy số trang*/
        [Route("NumberOfPage")]
        [HttpGet]
        public ActionResult GetNumberOfPage(int size)
        {
            int numberOfPost = _postService.NumberOfPublishedPosts();
            int numberOfPage = numberOfPost / size + 1;
            if (numberOfPost % size == 0) numberOfPage = numberOfPost / size;
            return Ok(new ResponseModel(ErrorConstant.SucceedCode, numberOfPage.ToString()));
        }
    }
}
