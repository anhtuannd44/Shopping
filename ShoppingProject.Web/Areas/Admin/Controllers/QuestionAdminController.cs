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
    public class QuestionAdminController : AdminBaseController
    {
        private readonly IQuestionService _questionService;
        private ILogger _logger;
        public QuestionAdminController(
            IQuestionService questionService,
            ILogger<QuestionAdminController> logger
            )
        {
            _questionService = questionService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _questionService.GetAllQuestion();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Reply,Status")] Question question)
        {
            if (!ModelState.IsValid)
                return View(question);
            try
            {
                await _questionService.CreateQuestion(question);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                ModelState.AddModelError("Error", "Có lỗi khi tạo câu hỏi, vui lòng kiểm tra và thử lại");
                return View(question);
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var model = await _questionService.GetQuestionById(id.Value);
            if (model == null)
                return NotFound();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionId,Title,Reply,Status")] Question question, string[] selectImage)
        {
            if (id != question.QuestionId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _questionService.UpdateQuestion(question);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Có lỗi khi sửa câu hỏi, vui lòng kiểm tra và thử lại");
                    ModelState.AddModelError(string.Empty, "Có lỗi khi sửa câu hỏi, vui lòng kiểm tra và thử lại");
                    throw;
                }
            }
            return View(question);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                await _questionService.RemoveQuestion(id);
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
