using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Domain.Enums;
using ShoppingProject.Service.Interface;
using ShoppingProject.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Areas.Admin.Controllers
{
    public class OrderAdminController : AdminBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ILogger<OrderAdminController> _logger;

        public OrderAdminController(
            IOrderService oderService,
            IProductService productService,
            UserManager<ApplicationUser> userManager,
            ILogger<OrderAdminController> logger
            )
        {
            _orderService = oderService;
            _productService = productService;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index(OrderStatus? status = null, int page = 1, int? pageSize = 10)
        {
            var model = new OrderViewModel
            {
                PageSize = pageSize,
                Data = await _orderService.GetAllPaging(status, page, pageSize.Value)
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            var model = await _orderService.GetOrderById(id);
            if (model == null)
                return NotFound();
            ViewData["ProductList"] = await _productService.GetAllProducts();
            return View(model);            
        }
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("OrderId,CustomerName,CustomerEmail,CustomerPhoneNumber,CustomerAddress,CustomerMessage,PaymentMethod,OrderStatus,OrderDetails")] Order order)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            if (order.OrderDetails.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Đơn hàng phải có ít nhất 1 sản phẩm");
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }    
            await _orderService.UpdateOrder(order);
            return new OkObjectResult(order);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _productService.GetAllProducts();
            return new OkObjectResult(result);
        }
    }
}
