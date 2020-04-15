using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechShare.Models
{
    public class SignupModel
    {
        [Required(ErrorMessage ="Tên là thông tin bắt buộc")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Họ là thông tin bắt buộc")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage ="Nickname là thông tin bắt buộc")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email là thông tin bắt buộc")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Giới tính là thông tin bắt buộc")]
        public int Gender { get; set; }

        [Required(ErrorMessage = "Nghề nghiệp là thông tin bắt buộc")]
        public string Job { get; set; }

        [Required(ErrorMessage = "Mật khẩu là thông tin bắt buộc")]        
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Nhập lại mật khẩu là thông tin bắt buộc")]        
        public string ConfirmPassword { get; set; }
    }
}
