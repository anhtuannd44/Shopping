using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Interface;
using ShoppingProject.Utilities.Enums;
using ShoppingProject.Utilities.Helper;
using ShoppingProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ShoppingProject.Data;
using ShoppingProject.Data.Interface;

namespace ShoppingProject.Web.Areas.Admin.Controllers
{
    public class PostCategoryAdminController : AdminBaseController
    {
        private readonly IPostService _postService;
        private readonly ApplicationDbContext _context;
        public PostCategoryAdminController(
            IPostService postService,
            IApplicationDbContext context
            )
        {
            _context = context as ApplicationDbContext;
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _postService.GetAllCategory();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Title,Content,Keyword,Slug,CoverUrl,Status")] PostCategory category)
        {
            if (ModelState["Slug"].ValidationState == ModelValidationState.Invalid || string.IsNullOrEmpty(category.Slug))
            {
                category.Slug = string.IsNullOrEmpty(category.Slug) ? Utils.GenerateSlug(category.Title) : category.Slug;
                ModelState.SetModelValue("Slug", new ValueProviderResult(category.Slug));
                ModelState.Clear();
                TryValidateModel(category);
            }

            if (await _postService.IsSlugCategoryExisted(category.Slug))
                ModelState.AddModelError(nameof(category.Slug), "Đường dẫn đã tồn tại");

            if (ModelState.IsValid)
            {
                await _postService.AddCategoryToDb(category);
                return RedirectToAction(nameof(Index));
            }

            var listcategory = await _postService.GetAllCategory();
            listcategory.Insert(0, new PostCategory()
            {
                Title = "Không có danh mục cha",
                CategoryId = -1
            });
            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var category = await _postService.GetCategoryItem(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,ParentId,Title,Content,Keyword,Slug,CoverUrl,Status")] PostCategory category)
        {
            if (id != category.CategoryId)
                return NotFound();
            if (ModelState["Slug"].ValidationState == ModelValidationState.Invalid || string.IsNullOrEmpty(category.Slug))
            {
                category.Slug = string.IsNullOrEmpty(category.Slug) ? Utils.GenerateSlug(category.Title) : category.Slug;
                ModelState.SetModelValue("Slug", new ValueProviderResult(category.Slug));
                ModelState.Clear();
                TryValidateModel(category);
            }

            if (await _postService.IsSlugCategoryExisted(category.Slug, category.CategoryId))
                ModelState.AddModelError(nameof(category.Slug), "Đường dẫn đã tồn tại");
            if (ModelState.IsValid)
            {
                try
                {
                    await _postService.UpdateCategory(category);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!await _postService.CategoryExists(category.CategoryId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            var listcategory = await _postService.GetAllCategory();
            listcategory.Insert(0, new PostCategory()
            {
                Title = "Không có danh mục cha",
                CategoryId = -1
            });
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            var category = await _postService.GetCategoryItem(id);
            if (category == null)
            {
                return Json(new ResultViewModel(CustomStatusCode.NotFound, "Thất bại! Danh mục không tồn tại"));
            }
            try
            {
                await _postService.RemoveCategory(category);
                return Json(new ResultViewModel(CustomStatusCode.Success, "Thành công! Đã xóa dữ liệu"));
            }
            catch (DbUpdateException ex)
            {
                //_logger.Log(LogLevel.Error, ex, ex.Message);
                return Json(new ResultViewModel(CustomStatusCode.InternalServerError, "Thất bại! Có lỗi xảy ra khi xóa dữ liệu"));
            }
        }
    }
}
