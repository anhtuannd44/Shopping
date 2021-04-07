using Microsoft.AspNetCore.Mvc;

namespace ShoppingProject.Controllers
{
    [Route("file-manager")]
    public class FileManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}