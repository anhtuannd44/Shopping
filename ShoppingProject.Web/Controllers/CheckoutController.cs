using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Domain.Enums;
using ShoppingProject.Service.Interface;
using ShoppingProject.Service.Model;
using ShoppingProject.Utilities.Enums;
using ShoppingProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Controllers
{
    [Route("hoan-tat-don-hang")]
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

        public IActionResult Index()
        {
            var session = HttpContext.Session.GetString("cart");
            if (session == null)
                return RedirectToAction(nameof(Index), "Home");
            return View();
        }
        public async Task<IActionResult> Index([Bind("CustomerName,CustomerAddress,CustomerPhone,CustomerMessage")] Order order)
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
                    return RedirectToAction(nameof(Success));
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
        public IActionResult Success()
        {
            return View();
        }
    }
}
