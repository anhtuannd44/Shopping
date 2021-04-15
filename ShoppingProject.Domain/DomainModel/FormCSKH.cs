using ShoppingProject.Domain.Enums;
using ShoppingProject.Utilities.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Domain.DomainModels
{
    [Table("FormCSKH")]
    public class FormCSKH
    {
        public FormCSKH()
        {
        }

        [Key]
        public int Id { set; get; }

        [Display(Name = "Nội dung", Prompt = "Nội dung")]
        public string Email { set; get; }
        [Display(Name = "Hình ảnh", Prompt = "Hình ảnh")]
        public
    }
}
