using System.ComponentModel.DataAnnotations;

namespace ShoppingProject.Domain.Enums
{
    public enum OrderStatus
	{
        [Display(Name = "Đơn mới")]
        New = 0,
        [Display(Name = "Đang xử lý")]
        InProgress = 1,
        [Display(Name = "Trả lại hàng")]
        Returned = 2,
        [Display(Name = "Đã hủy")]
        Cancelled =3,
        [Display(Name = "Đã hoàn tất")]
        Completed = 4
    }
}