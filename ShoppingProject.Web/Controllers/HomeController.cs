using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Service.Interface;
using ShoppingProject.Utilities.Enums;
using ShoppingProject.Web.Models;
using ShoppingProject.Web.Models.Home;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISettingService _settingService;
        public HomeController
            (
                ISettingService settingService
            )
        {
            _settingService = settingService;
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

        public IActionResult Error()
        {
            string message = "Xin lỗi! Có gì đó bất ngờ xảy ra. Vui lòng liên hệ quản trị viên";
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = new string[] { message } });
        }
    }
}
