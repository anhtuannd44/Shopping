using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Domain.DomainModels
{
    [Table("District")]
    public partial class District
    {
        public District()
        {
            Wards = new HashSet<Ward>();
        }

        [Key]
        public int DistrictId { set; get; }

        [Display(Name = "Tên")]
        public string Name { set; get; }
        [Display(Name = "Loại")]
        public string Type { get; set; }
        [Display(Name = "Mã tỉnh")]
        public int? ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public virtual Province Provinces { get; set; } 
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
