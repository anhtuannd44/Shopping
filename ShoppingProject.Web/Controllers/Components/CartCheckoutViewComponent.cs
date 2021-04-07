using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingProject.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Controllers.Components
{
    public class CartCheckoutViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var session = HttpContext.Session.GetString("cart");
            var cart = new List<ShoppingCartItem>();
            if (session != null)
                cart = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(session);
            return View(cart);
        }
    }
}
