using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingProject.Domain.Enums
{
    public enum PaymentMethod
    {
        [Description("Thanh toán khi nhận hàng")]
        CashOnDelivery,
        [Description("Chuyển khoản")]
        OnlineBanking
    }
}