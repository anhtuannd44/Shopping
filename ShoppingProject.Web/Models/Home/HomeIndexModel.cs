
using ShoppingProject.Domain.DomainModels;
using System.Collections.Generic;

namespace ShoppingProject.Web.Models.Home
{
    public class HomeIndexModel
    {
        public List<Setting> Setting { get; set; }
        public List<Slider> Slider { get; set; }
    }
}
