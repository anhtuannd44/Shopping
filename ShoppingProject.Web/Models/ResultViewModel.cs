using ShoppingProject.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ShoppingProject.Web.Models
{
    public class ResultViewModel
    {
        public ResultViewModel(CustomStatusCode code, string message)
        {
            Status = (int)code;
            Message = string.IsNullOrEmpty(message) ? string.Empty : message;
        }

        public int Status;
        public string Message;
    }
}
