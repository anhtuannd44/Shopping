using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Utilities;
using System.Collections.Generic;

namespace ShoppingProject.Web.Models
{
    public class QuestionViewModel
    {
        public PagedResult<Question> Data { get; set; }

        public int? PageSize { set; get; }

        public List<SelectListItem> PageSizes { get; } = new List<SelectListItem>
        {
            new SelectListItem(){Value = "12",Text = "12"},
            new SelectListItem(){Value = "24",Text = "24"},
            new SelectListItem(){Value = "48",Text = "48"},
        };
    }
}
