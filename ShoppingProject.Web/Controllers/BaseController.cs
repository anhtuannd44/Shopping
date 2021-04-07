using ShoppingProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using ShoppingProject.Data.Logging;

namespace ShoppingProject.Web.Controllers
{
    public abstract class BaseController<T> : Controller
    {
        private IAppLogger<T> _logger;
        protected IAppLogger<T> Logger => _logger ?? (_logger = HttpContext?.RequestServices.GetService<IAppLogger<T>>());
        
        protected void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            Logger.Log(LogLevel.Error, filterContext.Exception, filterContext.Exception.Message);
            string msg = "We're sorry. Something unexpected happend! Please try again later or contact us.";
            filterContext.Result = View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = new string[] { msg } });
        }
    }
}
