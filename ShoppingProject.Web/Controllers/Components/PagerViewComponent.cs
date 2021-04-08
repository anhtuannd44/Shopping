using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Utilities;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
