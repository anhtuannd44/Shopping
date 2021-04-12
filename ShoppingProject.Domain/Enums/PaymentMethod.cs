using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingProject.Domain.Enums
{
    public enum PaymentMethod
    {
        [Display(Name = "Thanh toán khi nhận hàng")]
        CashOnDelivery,
        [Display(Name = "Chuyển khoản")]
        OnlineBanking
    }
}