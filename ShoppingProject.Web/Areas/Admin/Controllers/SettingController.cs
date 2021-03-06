using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Domain.Enums;
using ShoppingProject.Service.Interface;
using ShoppingProject.Utilities.Enums;
using ShoppingProject.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Areas.Admin.Controllers
{
    public class SettingController : AdminBaseController
    {
        private readonly ISettingService _settingService;
        private readonly ILogger<SettingController> _logger;

        public SettingController(
                ISettingService settingService,
                ILogger<SettingController> logger
            )
        {
            _settingService = settingService;
            _logger = logger;
        }
        public async Task<IActionResult> SettingHomepage()
        {
            //await _settingService.CreateDataSettingHompage();
            var model = new HomeIndexModel()
            {
                Setting = await _settingService.GetListSettingList(SettingType.Homepage),
                Slider = await _settingService.GetAllSlider()
            };
            return View(model);
        }
        public async Task<IActionResult> UpdateSetting(List<Setting> setting, List<Slider> slider)
        {
            try
            {
                setting.ForEach(a => { a.Type = SettingType.Homepage; a.Status = Status.Public; });
                await _settingService.UpdateSetting(setting, slider);
                return new OkObjectResult(setting);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Có lỗi xảy ra khi cập nhật dữ liệu");
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi cập nhật dữ liệu");
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
        }
        public async Task<IActionResult> Slider()
        {
            var model = await _settingService.GetAllSlider();
            return View(model);
        }
    }
}
