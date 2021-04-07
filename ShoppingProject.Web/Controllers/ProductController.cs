using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Controllers
{
    [Route("/san-pham")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMemoryCache _cache;
        private readonly IProductService _productService;
        public ProductController(
            ILogger<ProductController> logger,
            IMemoryCache cache,
            IProductService productService
            )
        {
            _logger = logger;
            _cache = cache;
            _productService = productService;
        }

        [Route("{slug}.html", Name = "viewonepost")]
        public async Task<IActionResult> DisplayProduct()
        {
            string Slug = (string)Request.RouteValues["slug"];
            if (string.IsNullOrEmpty(Slug))
                return NotFound();
            var post = await _productService.GetProductBySlug(Slug);
            if (post == null)
                return NotFound();
            return View(post);
        }
    }
}
