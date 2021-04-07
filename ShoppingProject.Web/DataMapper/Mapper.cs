using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Web.Models.Account;
using System;
using System.Linq;

namespace ShoppingProject.Web.DataMapper
{
    public class Mapper
    {
        public ApplicationUser AccountRegisterModelToApplicationUser(AccountRegisterModel login)
        {
            return new ApplicationUser
            {
                FullName = login.FullName,
                Address = login.Address,
                Email = login.Email,
                AvatarUrl = login.AvatarUrl,
                DateCreated = DateTime.Now,
                UserName = login.Email,
                PhoneNumber = login.PhoneNumber,
            };
        }
    }
}
