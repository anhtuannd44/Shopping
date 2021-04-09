using ShoppingProject.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Domain.DomainModels
{
    [Table("Question")]
    public class Question
    {
        public Question()
        {
        }

        [Key]
        public int QuestionId { set; get; }
        [Required(ErrorMessage = "Bạn chưa nhập câu hỏi")]
        [Display(Name = "Câu hỏi", Prompt = "Nhập câu hỏi")]
        public string Title { set; get; }
        [Display(Name = "Câu trả lời")]
        [Required(ErrorMessage = "Bạn chưa nhập câu trả lời")]
        public string Reply { set; get; }
        [Required]
        public Status Status { get; set; }
    }
}
