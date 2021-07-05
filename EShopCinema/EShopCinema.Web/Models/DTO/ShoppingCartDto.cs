using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopCinema.Web.Models.Domain;

namespace EShopCinema.Web.Models.DTO
{
    public class ShoppingCartDto
    {
        public List<CinemaTicketInShoppingCart> CinemaTickets { get; set; }
        public double TotalPrice { get; set; }
    }
}
