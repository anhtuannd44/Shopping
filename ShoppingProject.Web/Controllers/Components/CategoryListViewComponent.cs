using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Service.Interface;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Controllers.Components
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly IPostService _postService;
        public CategoryListViewComponent(
                IPostService postService
            )
        {
            _postService = postService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _postService.GetAllCategory();
            return View(model);
        }
    }
}
