using EShopCinema.Domain.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopCinema.Domain.Identity
{
    public class EShopApplicationCinemaUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        //public virtual CinemaShoppingCart CinemaCart { get; set; }
        public virtual CinemaShoppingCart UserCart { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
