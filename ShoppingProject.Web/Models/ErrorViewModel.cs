using System;
using System.Collections.Generic;

namespace ShoppingProject.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string[] Message { get; set; }
    }
}