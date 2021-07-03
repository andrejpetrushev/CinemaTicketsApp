﻿using EShopCinema.Web.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopCinema.Web.Models.Identity
{
    public class EShopApplicationCinemaUser : IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Address { get; set; }
        public virtual CinemaShoppingCart CinemaCart { get; set; }
    }
}