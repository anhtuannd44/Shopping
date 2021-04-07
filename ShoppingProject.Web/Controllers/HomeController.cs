using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Service.Interface;
using ShoppingProject.Web.DataMapper;
using ShoppingProject.Web.Models;
using System.Diagnostics;

namespace ShoppingProject.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [Route("/")]
        public IActionResult Index()
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
