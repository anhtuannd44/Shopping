using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Interface;
using ShoppingProject.Utilities.Enums;
using ShoppingProject.Utilities.Helper;
using ShoppingProject.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Areas.Admin.Controllers
{
    public class ProductAdminController : AdminBaseController
    {
        private readonly IProductService _productService;
        private ILogger _logger;
        public ProductAdminController(
            IProductService productService,
            ILogger<ProductAdminController> logger
            )
        {
            _productService = productService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _productService.GetAllProducts();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,Keyword,Slug,CoverUrl,Status,Price,PromotionPrice,CoverUrl,Description,ShortDescription,DeliveryDescription")] Product product, string[] selectImage)
        {
            if (ModelState["Slug"].ValidationState == ModelValidationState.Invalid || string.IsNullOrEmpty(product.Slug))
            {
                product.Slug = string.IsNullOrEmpty(product.Slug) ? Utils.GenerateSlug(product.Title) : product.Slug;
                ModelState.SetModelValue("Slug", new ValueProviderResult(product.Slug));
                ModelState.Clear();
                TryValidateModel(product);
            }
            if (await _productService.IsSlugProductExisted(product.Slug))
                ModelState.AddModelError(nameof(product.Slug), "Đường dẫn đã tồn tại");

            if (product.PromotionPrice.HasValue && product.PromotionPrice >= product.Price)
                ModelState.AddModelError(nameof(product.PromotionPrice), "Giá khuyến mãi phải nhỏ hơn giá gốc");

            product.DateCreated = DateTime.Now;
            if (!ModelState.IsValid)
                return View(product);
            try
            {
                await _productService.AddProductToDb(product, selectImage);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                ModelState.AddModelError("Error", "Có lỗi khi tạo sản phẩm, vui lòng kiểm tra và thử lại");
                return View(product);
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var model = await _productService.GetProductById(id.Value);
            if (model == null)
                return NotFound();
            ViewData["ProductAlbum"] = model.ProductImages.Select(a => a.ImagePath).ToArray();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Title,Content,Keyword,Slug,CoverUrl,Status,Price,PromotionPrice,CoverUrl,Description,ShortDescription,DeliveryDescription")] Product product, string[] selectImage)
        {
            if (id != product.ProductId)
                return NotFound();
            if (ModelState["Slug"].ValidationState == ModelValidationState.Invalid || string.IsNullOrEmpty(product.Slug))
            {
                product.Slug = string.IsNullOrEmpty(product.Slug) ? Utils.GenerateSlug(product.Title) : product.Slug;
                ModelState.SetModelValue("Slug", new ValueProviderResult(product.Slug));
                ModelState.Clear();
                TryValidateModel(product);
            }

            if (await _productService.IsSlugProductExisted(product.Slug, product.ProductId))
                ModelState.AddModelError(nameof(product.Slug), "Đường dẫn đã tồn tại");
            if (product.PromotionPrice.HasValue && product.PromotionPrice >= product.Price)
                ModelState.AddModelError(nameof(product.PromotionPrice), "Giá khuyến mãi phải nhỏ hơn giá gốc");

            if (!ModelState.IsValid)
            {
                ViewData["ProductAlbum"] = selectImage;
                return View(product);
            }
            try
            {
                await _productService.UpdateProductItem(product, selectImage);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                ModelState.AddModelError(string.Empty, "Có lỗi khi sửa sản phẩm, vui lòng kiểm tra và thử lại");
                ViewData["ProductAlbum"] = selectImage;
                return View(product);
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int? id)
        {
            var post = await _productService.GetProductById(id.Value);
            if (post == null)
            {
                return Json(new ResultViewModel(CustomStatusCode.NotFound, "Thất bại! Bài viết không tồn tại"));
            }
            try
            {
                await _productService.RemoveProduct(post);
                return Json(new ResultViewModel(CustomStatusCode.Success, "Thành công! Đã xóa dữ liệu"));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Có lỗi khi xóa sản phẩm");
                return Json(new ResultViewModel(CustomStatusCode.InternalServerError, "Thất bại! Có lỗi xảy ra khi xóa bài viết"));
            }
        }
    }
}
