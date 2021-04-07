using ShoppingProject.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Domain.DomainModels
{
    [Table("ProductImage")]
    public class ProductImage
    {
        public ProductImage()
        {
        }

        [Key]
        [Column(Order = 0)]
        public int ProductId { set; get; }
        [Key]
        [Column(Order = 1)]
        public string ImagePath { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }
    }
}
