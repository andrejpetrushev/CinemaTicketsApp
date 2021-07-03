using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopCinema.Web.Models.Domain
{
    public class CinemaTicketInShoppingCart
    {
        public Guid CinemaTicketId { get; set; }
        public TicketCinema TicketCinema { get; set; }
        public Guid ShoppingCartId { get; set; }
        public CinemaShoppingCart CinemaShoppingCart { get; set; }
        //public int TicketsQuantity { get; set; }

    }
}
