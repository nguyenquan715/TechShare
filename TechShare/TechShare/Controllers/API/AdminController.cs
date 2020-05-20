using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechShare.Infra;
using TechShare.Entity;
using TechShare.Models;

namespace TechShare.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = RoleConstant.Admin)]
    public class AdminController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /*Lấy về danh sách các nhân viên*/
        [Route("employee")]
        [HttpGet]
        public async Task<IEnumerable<AppUser>> GetAllEmployees()
        {
            List<AppUser> employees = new List<AppUser>();
            List<AppUser> users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                bool role = await _userManager.IsInRoleAsync(user, RoleConstant.Employee);
                if (role)
                {
                    employees.Add(user);
                }
            }
            return employees;
        }

        /*Lấy về danh sách các thành viên*/
        [Route("member")]
        [HttpGet]
        public async Task<IEnumerable<AppUser>> GetAllMembers()
        {
            List<AppUser> members = new List<AppUser>();
            List<AppUser> users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                bool role = await _userManager.IsInRoleAsync(user, RoleConstant.Member);
                if (role)
                {
                    members.Add(user);
                }
            }
            return members;
        }


        /*Lấy thông tin user theo ID*/
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetUserByID(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user!=null) return Ok(user);
            var response = new ResponseModel();
            response.AddError(ErrorConstant.NoContentCode, ErrorConstant.NoContentMess);            
            return Ok(response);
        }

        /*Thêm một thành viên thành nhân viên*/
        [Route("MemToEmp/{id}")]
        [HttpPut]
        public async Task<ActionResult> MemberToEmployee(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                /*Xóa role member*/
                var res = await _userManager.RemoveFromRoleAsync(user, RoleConstant.Member);
                if (res.Succeeded)
                {
                    /*Thêm role employee*/
                    var result = await _userManager.AddToRoleAsync(user, RoleConstant.Employee);
                    if (result.Succeeded)
                    {                        
                        return Ok(new ResponseModel(ErrorConstant.SucceedCode,ErrorConstant.SucceedMess));
                    }
                    return Ok(SetOfError(result));
                }
                return Ok(SetOfError(res));
            }
            return Ok(new ResponseModel(ErrorConstant.NoContentCode,ErrorConstant.NoContentMess));
        }

        /*Chuyển một nhân viên xuống thành viên*/
        [Route("EmpToMem/{id}")]
        [HttpPut]
        public async Task<ActionResult> EmployeeToMember(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                /*Xóa role employee*/
                var res = await _userManager.RemoveFromRoleAsync(user, RoleConstant.Employee);
                if (res.Succeeded)
                {
                    /*Thêm role member*/
                    var result = await _userManager.AddToRoleAsync(user, RoleConstant.Member);
                    if (result.Succeeded)
                    {
                        return Ok(new ResponseModel(ErrorConstant.SucceedCode, ErrorConstant.SucceedMess));
                    }
                    return Ok(SetOfError(result));
                }
                return Ok(SetOfError(res));
            }
            return Ok(new ResponseModel(ErrorConstant.NoContentCode, ErrorConstant.NoContentMess));
        }

        /*Chặn/bỏ chặn thành viên*/
        [Route("block/{id}")]
        [HttpPut]
        public async Task<ActionResult> BlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Blocked = !user.Blocked;
                var res = await _userManager.UpdateAsync(user);
                if (res.Succeeded) return Ok(new ResponseModel(ErrorConstant.SucceedCode, ErrorConstant.SucceedMess));
                return Ok(SetOfError(res));
            }
            return Ok(new ResponseModel(ErrorConstant.NoContentCode, ErrorConstant.NoContentMess));
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