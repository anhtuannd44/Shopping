using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Interface;
using ShoppingProject.Utilities.Enums;
using ShoppingProject.Utilities.Helper;
using ShoppingProject.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ShoppingProject.Web.Areas.Admin.Controllers
{
    public class PostAdminController : AdminBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPostService _postService;
        private readonly ILogger _logger;

        public PostAdminController(
            IPostService postService,
            UserManager<ApplicationUser> userManager,
            ILogger<PostAdminController> logger
            )
        {
            _postService = postService;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _postService.GetAllPost();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _postService.GetAllCategory(), "CategoryId", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Slug,Content,Keyword,CoverUrl,Status,CategoryId")] Post post)
        {
            post.DateCreated = DateTime.Now;
            post.AuthorId = _userManager.GetUserAsync(User).Result.Id;

            if (ModelState["Slug"].ValidationState == ModelValidationState.Invalid || string.IsNullOrEmpty(post.Slug))
            {
                post.Slug = string.IsNullOrEmpty(post.Slug) ? Utils.GenerateSlug(post.Title) : post.Slug;
                ModelState.SetModelValue("Slug", new ValueProviderResult(post.Slug));
                ModelState.Clear();
                TryValidateModel(post);
            }
            if (await _postService.IsSlugPostExisted(post.Slug))
                ModelState.AddModelError(nameof(post.Slug), "Đường dẫn đã tồn tại");

            if (ModelState.IsValid)
            {
                await _postService.AddPostToDb(post);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _postService.GetAllCategory(), "CategoryId", "Title", post.CategoryId);
            return View(post);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var post = await _postService.GetPostItem(id);
            if (post == null)
                return NotFound();
            ViewData["CategoryId"] = new SelectList(await _postService.GetAllCategory(), "CategoryId", "Title", post.CategoryId);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Description,Slug,Content,Keyword,CoverUrl,Status,CategoryId")] Post post)
        {
            if (id != post.PostId)
                return NotFound();

            if (ModelState["Slug"].ValidationState == ModelValidationState.Invalid || string.IsNullOrEmpty(post.Slug))
            {
                post.Slug = string.IsNullOrEmpty(post.Slug) ? Utils.GenerateSlug(post.Title) : post.Slug;
                ModelState.SetModelValue("Slug", new ValueProviderResult(post.Slug));
                ModelState.Clear();
                TryValidateModel(post);
            }
            if (await _postService.IsSlugPostExisted(post.Slug, post.PostId))
                ModelState.AddModelError(nameof(post.Slug), "Đường dẫn đã tồn tại");

            if (ModelState.IsValid)
            {
                try
                {
                    await _postService.UpdatePost(post);
                }
                catch (DbUpdateConcurrencyException DbEx)
                {
                    _logger.LogInformation(DbEx, "Có lỗi khi lưu dữ liệu");
                    ModelState.AddModelError(string.Empty, "Có lỗi khi lưu dữ liệu");
                    if (!await _postService.PostExists(post.PostId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _postService.GetAllCategory(), "CategoryId", "Title", post.CategoryId);
            return View(post);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int? id)
        {
            var post = await _postService.GetPostItem(id);
            if (post == null)
                return Json(new ResultViewModel(CustomStatusCode.NotFound, "Thất bại! Bài viết không tồn tại"));
            try
            {
                await _postService.RemovePost(post);
                return Json(new ResultViewModel(CustomStatusCode.Success, "Thành công! Đã xóa dữ liệu"));
            }
            catch (Exception)
            {
                return Json(new ResultViewModel(CustomStatusCode.Success, "Thất bại! Có lỗi xảy ra khi xóa bài viết"));
            }
        }
    }
}
