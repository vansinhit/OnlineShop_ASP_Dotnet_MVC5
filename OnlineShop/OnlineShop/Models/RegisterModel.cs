using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { get; set; }


        [Required(ErrorMessage = "Yêu cầu điền tên đăng nhập.")]
        [Display(Name = "Tên Đăng Nhập")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Yêu cầu điền mật khẩu.")]
        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu phải lớn hơn 6")]
        public string Password { get; set; }



        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string ConfimPassword { get; set; }


        [Display(Name = "Họ tên")]
        [Required(ErrorMessage ="Vui lòng điền họ và tên")]
        public string Name { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage ="Vui lòng điền Địa chỉ")]
        public string Address { get; set; }
       
        
        [Display(Name = "Email")]
        [Required(ErrorMessage ="Vui lòng điền Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage ="Vui lòng điền số điện thoại")]
        public string Phone { get; set; }
    }
}