using EShopCinema.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopCinema.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<CinemaTicketInShoppingCart> CinemaTickets { get; set; }
        public double TotalPrice { get; set; }
    }
}
