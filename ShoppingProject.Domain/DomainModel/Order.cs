using ShoppingProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Domain.DomainModels
{
    [Table("Order")]
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        [Key]
        [Display(Name = "Mã đơn hàng")]
        public string OrderId { get; set; }
        [Display(Name = "Tên của bạn*")]
        [Required(ErrorMessage = "Bạn chưa nhập tên của mình")]
        public string CustomerName { get; set; }
        [Display(Name = "Địa chỉ giao hàng*")]
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ của mình")]
        public string CustomerAddress { get; set; }
        [Display(Name = "Số điện thoại*")]
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        public string CustomerPhoneNumber { get; set; }
        [Display(Name = "Email*")]
        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng Email")]
        public string CustomerEmail { get; set; }
        [Display(Name = "Thêm ghi chú cho đơn hàng của bạn")]
        public string CustomerMessage { get; set; }
        [DefaultValue(PaymentMethod.CashOnDelivery)]
        [Display(Name = "Hình thức thanh toán")]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Thời gian đặt hàng")]
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        [DefaultValue(OrderStatus.New)]
        [Display(Name = "Trạng thái đơn hàng")]
        public OrderStatus OrderStatus { get; set; }

        public string CustomerId { get; set; }
        public virtual ApplicationUser Customer { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

    }
}
