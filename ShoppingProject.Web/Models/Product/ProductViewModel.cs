using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Utilities;

namespace ShoppingProject.Web.Models
{
    public class ProductViewModel
    {
        public PagedResult<Product> Data { get; set; }

        public int? PageSize { set; get; }
    }
}
