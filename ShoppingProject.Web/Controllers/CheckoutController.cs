using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Domain.Enums;
using ShoppingProject.Service.Interface;
using ShoppingProject.Service.Model;
using ShoppingProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Controllers
{
    [Route("dat-hang")]
    public class CheckoutController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController
            (
                IOrderService orderService,
                UserManager<ApplicationUser> userManager,
                ILogger<CheckoutController> logger
            )
        {
            _orderService = orderService;
            _userManager = userManager;
            _logger = logger;
        }
        [HttpGet]
        [Route("hoan-tat-don-hang")]
        public IActionResult Index()
        {

            var session = HttpContext.Session.GetString("cart");
            if (session == null)
                return RedirectToAction(nameof(Index), "Home");
            return View();
        }

        [HttpPost]
        [Route("hoan-tat-don-hang")]
        public async Task<IActionResult> Index([Bind("CustomerName,CustomerAddress,CustomerPhoneNumber,CustomerMessage,CustomerEmail")] Order order)
        {
            var session = HttpContext.Session.GetString("cart");
            if (session == null)
                return RedirectToAction(nameof(Index), "Home");
            var cart = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(session).Select(a => new ProductOrderWithQuantity() { ProductId = a.Product.ProductId, Quantity = a.Quantity }).ToList();

            if (ModelState.IsValid)
            {
                order.DateCreated = DateTime.Now;
                order.PaymentMethod = PaymentMethod.CashOnDelivery;
                if (User.Identity.IsAuthenticated)
                    order.CustomerId = _userManager.GetUserAsync(User).Result.Id;
                order.OrderStatus = OrderStatus.New;
                try
                {
                    await _orderService.CreateNewOrder(order, cart);
                    HttpContext.Session.Clear();
                    return RedirectToAction(nameof(Details), new { orderId = order.OrderId });
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex, "Có lỗi xảy ra khi đặt hàng");
                    ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi đặt hàng. Vui lòng thử lại");
                    throw;
                }
            }
            return View(order);
        }
        [Route("chi-tiet-don-hang")]
        public async Task<IActionResult> Details(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return NotFound();
            var order = await _orderService.GetOrderById(orderId);
            if (order == null)
                return NotFound();
            return View(order);
        }
    }
}
