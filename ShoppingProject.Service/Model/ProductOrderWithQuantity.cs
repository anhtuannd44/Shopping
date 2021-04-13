using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingProject.Service.Model
{
    public class ProductOrderWithQuantity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}
