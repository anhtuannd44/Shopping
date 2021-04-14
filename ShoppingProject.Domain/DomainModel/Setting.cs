using ShoppingProject.Domain.Enums;
using ShoppingProject.Utilities.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Domain.DomainModels
{
    [Table("Setting")]
    public class Setting
    {
        public Setting()
        {
        }

        [Key]
        public SettingKey Id { set; get; }

        [Display(Name = "Tiêu đề", Prompt = "Tiêu đề")]
        public string Title { set; get; }

        [Display(Name = "Nội dung", Prompt = "Nội dung")]
        public string Content { set; get; }
        [Display(Name = "Hình ảnh", Prompt = "Hình ảnh")]
        public string Image { set; get; }
        public string Link { get; set; }
        public SettingType Type { get; set; }
        [DefaultValue(Status.Public)]
        public Status Status { get; set; }
    }
}
