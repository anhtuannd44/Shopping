using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Service.Interface;
using ShoppingProject.Utilities.Enums;
using ShoppingProject.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        public const string CARTKEY = "cart";

        public CartController
            (
                IProductService productService
            )
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View();
        }

        [Route("add-to-cart")]
        [HttpGet]
        public async Task<JsonResult> Add(int productId, int quantity)
        {
            if (SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart") == null)
            {
                List<ShoppingCartItem> cart = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem { Product = await _productService.GetProductByIdNotInclude(productId), Quantity = quantity }
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<ShoppingCartItem> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart");
                int index = isExist(productId);
                if (index != -1)
                {
                    cart[index].Quantity += quantity;
                }
                else
                {
                    cart.Add(new ShoppingCartItem { Product = await _productService.GetProductByIdNotInclude(productId), Quantity = quantity });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return Json(new ResultViewModel(CustomStatusCode.Success, "Thành công, đã thêm vào giỏ hàng"));
        }
        [Route("MiniCart")]
        public IActionResult MiniCart()
        {
            return ViewComponent("MiniCart");
        }
        [HttpGet]
        [Route("remove")]
        public IActionResult Remove(int productId)
        {
            List<ShoppingCartItem> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart");
            int index = isExist(productId);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return Json(new ResultViewModel(CustomStatusCode.Success, "Thành công, đã xóa sản phẩm khỏi giỏ hàng"));
        }

        private int isExist(int id)
        {
            List<ShoppingCartItem> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
