using ShoppingProject.Utilities.Enums;
using System;
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

        [Display(Name = "Email", Prompt = "Nhập email")]
        [EmailAddress(ErrorMessage = "Email chưa chính xác")]
        [Required(ErrorMessage = "Bạn chưa nhập Email của mình")]
        public string Email { set; get; }

        [Display(Name = "Giới tính", Prompt = "Chọn giới tính")]
        [Required(ErrorMessage = "Bạn chưa chọn giới tính")]
        public Gentle Gentle { get; set; }

        [Display(Name = "Năm sinh", Prompt = "Năm sinh")]
        [Required(ErrorMessage = "Bạn chưa chọn năm sinh")]
        public int BirthYear { get; set; }

        public DateTime DateCreated { get; set; }
        public bool IsRead { get; set; }
    }
}
