using ShoppingProject.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Domain.DomainModels
{
    public class PostCategory
    {
        public PostCategory()
        {
            Posts = new HashSet<Post>();
        }
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Phải có tên danh mục")]
        [Display(Name = "Tên danh mục", Prompt = "Nhập tên danh mục")]
        public string Title { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Chi tiết thêm", Prompt = "Giải thích thêm hoặc để trống")]
        public string Content { set; get; }
        [RegularExpression(@"^[a-z0-9-]*$", ErrorMessage = "Chỉ dùng các ký tự [a-z0-9-]")]
        [Display(Name = "Đường dẫn SEO (VD: danh-muc-abc)", Prompt = "Nhập hoặc để trống tự phát sinh theo Title")]
        public string Slug { set; get; }
        [Display(Name = "Từ khóa SEO", Prompt = "Nhập nhiều từ khóa, cách nhau bằng dấu phẩy ','")]
        public string Keyword { get; set; }
        [Display(Name = "Hình đại diện")]
        public string CoverUrl { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
