using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Interface;
using ShoppingProject.Web.Models;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Controllers
{
    [Route("tin-tuc")]
    public class BlogController : Controller
    {
        private readonly IPostService _postService;
        private readonly ILogger<BlogController> _logger;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;
        public const int ITEMS_PER_PAGE = 10;
        public BlogController
            (
                IPostService postService,
                ILogger<BlogController> logger,
                IMemoryCache cache,
                IConfiguration configuration
            )
        {
            _postService = postService;
            _logger = logger;
            _cache = cache;
            _configuration = configuration;
        }
        public IActionResult Index(int? pageSize = 10, int page = 1)
        {
            var catalog = new BlogViewModel
            {
                PageSize = pageSize,
                Data = _postService.GetAllPaging(null, string.Empty, page, pageSize.Value),
                Category = new PostCategory()
            };
            return View(catalog);
        }
        [Route("{slug}")]
        public async Task<IActionResult> BlogCategory(int? pageSize = 10, int page = 1)
        {
            string slug = (string)Request.RouteValues["slug"];
            if (string.IsNullOrEmpty(slug))
                return RedirectToAction("Index");
            var category = await _postService.GetCategoryItemBySlug(slug);
            if (category == null)
                return NotFound();
            var catalog = new BlogViewModel
            {
                PageSize = pageSize,
                Data = _postService.GetAllPaging(null, string.Empty, page, pageSize.Value),
                Category = category
            };
            return View("Index",catalog);
        }

        [Route("{category}/{slug}")]
        public async Task<IActionResult> Details()
        {
            string slug = (string)Request.RouteValues["slug"];
            string categorySlug = (string)Request.RouteValues["category"];
            if (string.IsNullOrEmpty(slug))
                return NotFound();
            var blog = await _postService.GetPostItemBySlug(slug);
            if (blog == null || blog.Categories == null || blog.Categories.Slug != categorySlug)
                return NotFound();
            return View(blog);
        }
    }
}
