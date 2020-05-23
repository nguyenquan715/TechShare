using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                                return Ok(new ResponseModel(ErrorConstant.SucceedCode, ErrorConstant.SucceedMess));
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