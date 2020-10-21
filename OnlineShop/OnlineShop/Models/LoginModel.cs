using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class LoginModel
    {
        [Key]
        
        [Required(ErrorMessage ="Vui lòng điền tên đăng nhập")]
        public string Username { get; set; }


       
        [Required(ErrorMessage ="Vui lòng điền mật khẩu")]
        public string Password { get; set; }
    }
}