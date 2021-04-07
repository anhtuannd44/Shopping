using ShoppingProject.Domain.DomainModels;

namespace ShoppingProject.Web.Models
{
    public class ShoppingCartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
    
}