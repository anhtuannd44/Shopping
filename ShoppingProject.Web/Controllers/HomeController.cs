using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Interface;
using ShoppingProject.Utilities.Enums;
using ShoppingProject.Web.Models;
using ShoppingProject.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly ICSKHService _cskhService;
        private readonly ILogger<HomeController> _logger;
        public HomeController
            (
                ISettingService settingService,
                ICSKHService cskhService,
                ILogger<HomeController> logger
            )
        {
            _settingService = settingService;
            _cskhService = cskhService;
            _logger = logger;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexModel()
            {
                Setting = await _settingService.GetListSettingList(SettingType.Homepage),
                Slider = await _settingService.GetAllSlider()
            };
            return View(model);
        }
        [Route("ribmax-dam-chat-dan-ong")]
        public IActionResult Ribmax()
        {
            return View();
        }

        [Route("lien-he")]
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            string message = "Xin lỗi! Có gì đó bất ngờ xảy ra. Vui lòng liên hệ quản trị viên";
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = new string[] { message } });
        }

        [HttpPost]
        public async Task<IActionResult> TuVanSucKhoe([Bind("Email,Gentle,BirthYear")] FormCSKH data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    data.IsRead = false;
                    await _cskhService.CreateCSKH(data);
                    return new OkObjectResult(data);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Có lỗi khi thêm dữ liệu tư vấn khách hàng");
                    throw;
                }
            }
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return new BadRequestObjectResult(allErrors);
        }
    }
}
