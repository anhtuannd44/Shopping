using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingProject.Service.Interface;
using ShoppingProject.Utilities.Enums;
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
            var model = await _settingService.GetListSettingList(SettingType.Homepage);
            return View(model);
        }
    }
}
