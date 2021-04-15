using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Interface;
using ShoppingProject.Utilities.Enums;
using ShoppingProject.Web.Models;
using System;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Areas.Admin.Controllers
{
    public class FormCSKHController : AdminBaseController
    {
        private readonly ICSKHService _cskhService;
        public FormCSKHController
            (
                ICSKHService cskhService
            )
        {
            _cskhService = cskhService;
        }
        public async Task<IActionResult> Index(int page = 1, int? pageSize = 10)
        {
            var model = new CSKHViewModel
            {
                PageSize = pageSize,
                Data = await _cskhService.GetCSKHList(page, pageSize.Value)
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Gentle,BirthYear")] FormCSKH data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    data.IsRead = true;
                    await _cskhService.CreateCSKH(data);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra trong quá trình tạo dữ liệu");
                    throw;
                }
            }
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var model = await _cskhService.GetFormCSKHItem(id.Value);
            if (model == null)
                return NotFound();
            model.IsRead = true;
            await _cskhService.UpdateCSKH(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Email,Gentle,BirthYear")] FormCSKH data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    data.IsRead = true;
                    await _cskhService.UpdateCSKH(data);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra trong quá trình cập nhật dữ liệu");
                    throw;
                }
            }
            return View(data);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                await _cskhService.RemoveCSKH(id);
                return Json(new ResultViewModel(CustomStatusCode.Success, "Thành công! Đã xóa dữ liệu"));
            }
            catch (Exception ex)
            {
                return Json(new ResultViewModel(CustomStatusCode.InternalServerError, "Thất bại! Có lỗi xảy ra khi xóa bài viết"));
            }
        }
    }
}
