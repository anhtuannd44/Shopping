using ShoppingProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Domain.DomainModels
{
    [Table("Product")]
    public class Product
    {
        public Product()
        {
            ProductImages = new HashSet<ProductImage>();
            OrderDetails = new HashSet<OrderDetails>();
        }

        [Key]
        public int ProductId { set; get; }
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm")]
        [Display(Name = "Tên sản phẩm", Prompt = "Nhập tên sản phẩm")]
        public string Title { set; get; }
        [Display(Name = "Mô tả ngắn (Dành cho SEO)")]
        public string Description { set; get; }
        [Display(Name = "Đường dẫn tĩnh (VD: tieu-de-abc)", Prompt = "Nhập hoặc để trống tự phát sinh theo Tên sản phẩm")]
        [RegularExpression(@"^[a-z0-9-]*$", ErrorMessage = "Chỉ dùng các ký tự [a-z0-9-]")]
        public string Slug { set; get; }
        [Display(Name = "Chi tiết sản phẩm")]
        public string Content { set; get; }
        [Display(Name = "Mô tả sản phẩm")]
        public string ShortDescription { get; set; }
        [Display(Name = "Thông tin giao hàng")]
        public string DeliveryDescription { get; set; }
        [Display(Name = "Từ khóa SEO", Prompt = "Nhập nhiều từ khóa, cách nhau bằng dấu phẩy ','")]
        public string Keyword { get; set; }
        

        //Thông tin giá cả
        [Display(Name = "Giá bán", Prompt = "VD: 20000")]
        [Required(ErrorMessage = "Bạn chưa nhập giá cho sản phẩm")]
        [DefaultValue(0)]
        public int Price { get; set; }
        [Display(Name = "Giá khuyến mãi", Prompt = "VD: 20000")]
        public int? PromotionPrice { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { set; get; }
        [Display(Name = "Cập nhật lần cuối")]
        public DateTime? DateUpdated { set; get; }
        public string CoverUrl { get; set; }
        [Display(Name = "")]
        public int? ViewCount { get; set; }
        [Display(Name = "Trạng thái")]
        public Status Status { set; get; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        [NotMapped]
        public int Quantity { get; set; }
    }
}
