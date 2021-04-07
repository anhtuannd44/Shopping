using System.ComponentModel.DataAnnotations;

namespace ShoppingProject.Web.Models.Account
{
    public class AccountRegisterModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name ="Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bạn chưa xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Mật khẩu chưa trùng khớp")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string PasswordConfirmation { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Định dạng Email chưa chính xác")]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Họ và Tên")]
        [StringLength(255, ErrorMessage = "Họ và tên chỉ từ 2 đến 255 ký tự", MinimumLength = 2)]
        [Display(Name = "Họ và Tên")]
        public string FullName { get; set; }

        [Display(Name ="Hình đại diện")]
        public string AvatarUrl { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không hợp lệ")]
        [Required(ErrorMessage = "Bạn chưa nhập Số điện thoại")]
        [Display(Name ="Số điện thoại")]
        public string PhoneNumber { get; set; }

        public string ReturnUrl { get; set; }
    }
}
