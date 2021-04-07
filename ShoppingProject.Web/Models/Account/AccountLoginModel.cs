using System.ComponentModel.DataAnnotations;

namespace ShoppingProject.Web.Models.Account
{
    public class AccountLoginModel
    {

        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Email không hợp lệ")]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name ="Mật khẩu")]
        public string Password { get; set; }

        public string IncorrectInput { get; set; }

        public bool? RememberMe { get; set; }
        public string ReturnUrl { get; set; }

    }
}
