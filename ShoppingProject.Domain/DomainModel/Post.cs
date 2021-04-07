using ShoppingProject.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Domain.DomainModels
{
    [Table("Post")]
    public class Post
    {
        public Post()
        {
        }

        [Key]
        public int PostId { set; get; }
        [Required(ErrorMessage = "Bạn chưa nhập tiêu đề bài viết")]
        [Display(Name = "Tiêu đề", Prompt = "Nhập tiêu đề của bạn")]
        public string Title { set; get; }
        [Display(Name = "Mô tả ngắn")]
        public string Description { set; get; }
        [Display(Name = "Đường dẫn tĩnh (VD: tieu-de-abc)", Prompt = "Nhập hoặc để trống tự phát sinh theo Title")]
        [RegularExpression(@"^[a-z0-9-]*$", ErrorMessage = "Chỉ dùng các ký tự [a-z0-9-]")]
        public string Slug { set; get; }
        [Display(Name = "Nội dung")]
        public string Content { set; get; }
        [Display(Name = "Từ khóa SEO", Prompt = "Nhập nhiều từ khóa, cách nhau bằng dấu phẩy ','")]
        public string Keyword { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { set; get; }
        [Display(Name = "Cập nhật lần cuối")]
        public DateTime? DateUpdated { set; get; }
        public string CoverUrl { get; set; }
        [Display(Name = "Trạng thái")]
        public Status Status { set; get; }
        [Display(Name = "Tác giả")]
        public string AuthorId { set; get; }
        [Display(Name = "Chuyên mục")]
        public int? CategoryId { get; set; }
        public virtual ApplicationUser Author { set; get; }
        public virtual PostCategory Categories { get; set; }
    }
}
