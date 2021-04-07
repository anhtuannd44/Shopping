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
        [Display(Name = "Thêm ghi chú cho đơn hàng của bạn")]
        public string CustomerMessage { get; set; }
        [DefaultValue(PaymentMethod.CashOnDelivery)]
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        [DefaultValue(OrderStatus.New)]
        public OrderStatus OrderStatus { get; set; }

        public string CustomerId { get; set; }
        public virtual ApplicationUser Customer { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
