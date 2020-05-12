using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechShare.Infra;
using TechShare.Models;

namespace TechShare.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]    
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
            foreach(var user in _userManager.Users)
            {
                if(await _userManager.IsInRoleAsync(user, RoleConstant.Employee))
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
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, RoleConstant.Member))
                {
                    members.Add(user);
                }
            }
            return members;
        }


        /*Lấy thông tin user theo ID*/
        [Route("{id}")]
        [HttpGet]
        public async Task<AppUser> GetUserByID(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }
    }
}