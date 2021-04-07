using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ShoppingProject.Domain.DomainModels
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Posts = new HashSet<Post>();
            Orders = new HashSet<Order>();
        }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
        public string Address { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}