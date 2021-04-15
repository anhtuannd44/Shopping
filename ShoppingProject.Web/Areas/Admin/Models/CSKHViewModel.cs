using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Utilities;

namespace ShoppingProject.Web.Models
{
    public class CSKHViewModel
    {
        public PagedResult<FormCSKH> Data { get; set; }

        public int? PageSize { set; get; }

    }
}
