using ShoppingProject.Domain.Enums;
using ShoppingProject.Utilities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Domain.DomainModels
{
    [Table("Slider")]
    public class Slider
    {
        public Slider()
        {
        }

        [Key]
        public int Id { set; get; }

        [Display(Name = "Hình ảnh")]
        public string Image { set; get; }

        [Display(Name = "Đường dẫn tĩnh")]
        public string Link { set; get; }
    }
}
