using System.ComponentModel.DataAnnotations;

namespace ShoppingProject.Domain.Enums
{
    public enum Status
	{
		[Display(Name = "Đã đăng")]
		Public = 0,
		[Display(Name = "Chưa đăng")]
		Private = 1
	}
}