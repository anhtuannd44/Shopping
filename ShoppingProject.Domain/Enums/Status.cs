using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingProject.Domain.Enums
{
    public enum OrderStatus
	{
        [Description("Đơn mới")]
        New,
        [Description("Đang xử lý")]
        InProgress,
        [Description("Trả lại hàng")]
        Returned,
        [Description("Đã hủy")]
        Cancelled,
        [Description("Đã hoàn tất")]
        Completed
    }
}