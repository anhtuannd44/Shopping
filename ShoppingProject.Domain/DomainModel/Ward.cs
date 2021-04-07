using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Domain.DomainModels
{
    [Table("Ward")]
    public partial class Ward
    {
        public Ward()
        {
        }
        [Key] 
        public int WardId { set; get; }
        [Display(Name = "Tên")]
        public string Name { set; get; }
        [Display(Name = "Loại")]
        public string Type { get; set; }
        [Display(Name = "Mã Quận/Huyện/Thị xã")]
        public int? DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District Districts { get; set; }
    }
}
