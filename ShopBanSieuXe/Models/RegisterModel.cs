using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBanSieuXe.Models
{
    public class RegisterModel
    {
        [Key]
        public int Id { set; get; }
        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ và tên")]
        public string FullName { set; get; }
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
        public string UserName { set; get; }
        [Display(Name = "mật khẩu")]
        [StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessage = "Độ dài mật khẩu ít nhất 8 ký tự.")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string PassWord { set; get; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("PassWord", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassWord { set; get; }
        [Display(Name = "Địa chỉ Email")]
        [Required(ErrorMessage = "Yêu cầu nhập địa chỉ Email")]
        public string Email { set; get; }
        //[Display(Name = "Giới Tính")]
        //public int Gender { set; get; }
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Yêu cầu nhập số điệ thoại")]
        public string Phone { set; get; }

    }
}