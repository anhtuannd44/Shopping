using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Domain.DomainModels
{
    [Table("Province")]
    public partial class Province
    {
        public Province()
        {
            Districts = new HashSet<District>();
        }

        [Key]
        public int ProvinceId { set; get; }

        [Display(Name = "Tên")]
        public string Name { set; get; }

        [Display(Name = "Loại")]
        public string Type { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
