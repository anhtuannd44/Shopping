using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Domain.Enums;
using ShoppingProject.Service.Interface;
using ShoppingProject.Web.Models;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Controllers
{
    [Route("hoi-dap")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        public QuestionController
            (
                IQuestionService questionService
            )
        {
            _questionService = questionService;
        }

        public async Task<IActionResult> Index(int? pageSize = 10, int page = 1)
        {
            var catalog = new QuestionViewModel
            {
                PageSize = pageSize,
                Data = await _questionService.GetAllPaging(page, pageSize.Value)
            };
            return View("Index", catalog);
        }
    }
}
