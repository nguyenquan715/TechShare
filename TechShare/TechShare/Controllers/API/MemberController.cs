using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RabbitMQ.Client;
using TechShare.Entity;
using TechShare.Infra;
using TechShare.Models;
using TechShare.Service;

namespace TechShare.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles =RoleConstant.Member)]
    public class MemberController : ControllerBase
    {
        private IPostService _postService;
        private readonly UserManager<AppUser> _userManager;
        private static ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
        public MemberController(IPostService postService,UserManager<AppUser> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        /*Lấy ra thông tin tất cả thể loại*/
        [Route("categories")]
        [HttpGet]
        public IEnumerable<Categories> GetAllCategories()
        {
            return _postService.GetAllCategories();
        }

        /*Tạo bài viết mới*/
        [Route("newpost")]
        [HttpPost]
        public async Task<ActionResult> CreateNewPost([FromBody]PostModel model)
        {
            if (ModelState.IsValid)
            {
                //Kiểm tra id của user
                var user = await _userManager.FindByIdAsync(model.UserId);
                //Nếu user tồn tại
                if (user != null)
                {
                    //Kiểm tra role của user có phải là member không
                    if(await _userManager.IsInRoleAsync(user, RoleConstant.Member))
                    {
                        //Kiểm tra member có bị block không
                        if (!user.Blocked)
                        {
                            var id = Guid.NewGuid();
                            string userId= model.UserId;
                            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId"))){
                                userId = HttpContext.Session.GetString("userId");
                            }                           
                            Posts post = new Posts
                            {
                                Id = id,
                                Title = model.Title,
                                Status = model.Status,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now,
                                SubmitedAt=model.SubmitedAt,
                                PublishedAt = null,
                                Avatar=model.Avatar,
                                Content = model.Content,
                                UserId = userId
                            };
                            List<PostCategories> categories = new List<PostCategories>();
                            foreach (var categoryID in model.CategoriesID)
                            {
                                //Kiểm tra xem có tồn tại categoryId không
                                if (_postService.FindCategoryById(categoryID) == null) 
                                    return Ok(new ResponseModel(ErrorConstant.NoContentCode,ErrorConstant.NoContentMess));
                                categories.Add(new PostCategories
                                {
                                    PostId = id,
                                    CategoryId = categoryID
                                });
                            }
                            try
                            {
                                _postService.CreateNewPost(post, categories);
                                return Ok(new ResponseModel(ErrorConstant.SucceedCode, id.ToString()));
                            }
                            catch (Exception ex)
                            {
                                return Ok(new ResponseModel("", ex.Message));
                            }
                        }
                        //Member bị block
                        return Ok(new ResponseModel(ErrorConstant.UserBlockedCode, ErrorConstant.UserBlockedMess));
                    }
                    //User không phải role member
                    return Ok(new ResponseModel(ErrorConstant.NotInRoleCode, ErrorConstant.NotInRoleMess));
                }
                //UserID không tồn tại
                return Ok(new ResponseModel(ErrorConstant.NoContentCode, ErrorConstant.NoContentMess));
            }
            return Ok(new ResponseModel(ErrorConstant.InvalidModelCode, ErrorConstant.InvalidModelMess));
        }

        /*Gửi message đến 1 employee bất kì sau khi tạo bài viết thành công*/
        [Route("SendMess")]
        [HttpPost]
        public ActionResult SendMessToEmployee([FromBody]MessageModel message)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
                {
                    message.UserId = HttpContext.Session.GetString("userId");
                }
                /*Gửi dữ liệu qua RabbitMQ*/                
                using(var connection = factory.CreateConnection())
                {
                    using(var channel = connection.CreateModel())
                    {
                        Console.WriteLine("Channel ready!");
                        channel.QueueDeclare(
                            queue:"memtoemp_queue",
                            durable:true,
                            exclusive:false,
                            autoDelete:false,
                            arguments:null
                        );
                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                        channel.BasicPublish(exchange: "", routingKey: "memtoemp_queue", basicProperties: properties, body: body);
                        Console.WriteLine("Sent");
                        return Ok(new ResponseModel(ErrorConstant.SucceedCode, ErrorConstant.SucceedMess));
                    }
                }
            }
            return Ok(new ResponseModel(ErrorConstant.InvalidModelCode, ErrorConstant.InvalidModelMess));
        }

        /*Lấy ra danh sách bài viết theo user*/
        [Route("ListPosts")]
        [HttpGet]
        public async Task<ActionResult> GetListPostsOfUser(string userId, int pageNumber,int size)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                userId = HttpContext.Session.GetString("userId");
            }
            //Kiểm tra userId
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                int end = size * pageNumber;
                int begin = end - size + 1;
                try
                {
                    var result = _postService.GetListPostsOfUser(userId,begin,end);
                    return Ok(result);
                }catch(Exception ex)
                {
                    return Ok(new ResponseModel("", ex.Message));
                }
            }
            return Ok(new ResponseModel(ErrorConstant.NoContentCode, ErrorConstant.NoContentMess));
        }
        
        /*Xóa bài viết*/
        [Route("DeletePost")]
        [HttpDelete]
        public ActionResult DeletePost(Guid postId,string userId)
        {
            //Check session
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                userId = HttpContext.Session.GetString("userId");
            }
            //Check bài viết có phải của user hay không
            if (_postService.CheckPostOfUser(postId, userId) == 1)
            {
                try
                {
                    //Xóa bài viết
                    _postService.DeletePost(postId);
                    return Ok(new ResponseModel(ErrorConstant.SucceedCode, ErrorConstant.SucceedMess));
                }
                catch(Exception ex)
                {
                    return Ok(new ResponseModel("", ex.Message));
                }
            }
            return Ok(new ResponseModel(ErrorConstant.NotAuthorOfPostCode,ErrorConstant.NotAuthorOfPostMess));
        }
        private ResponseModel SetOfError(IdentityResult res)
        {
            var response = new ResponseModel();
            foreach (IdentityError err in res.Errors)
            {                
                response.AddError(err.Code, err.Description);
            }
            return response;
        }
    }
}