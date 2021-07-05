using System;
using System.Collections.Generic;
using System.Text;

namespace EShopCinema.Domain.Domain
{
    public class CinemaTicketInShoppingCart : BaseEntity
    {
        public Guid CinemaTicketId { get; set; }
        public TicketCinema TicketCinema { get; set; }
        public Guid ShoppingCartId { get; set; }
        public CinemaShoppingCart CinemaShoppingCart { get; set; }
        public int TicketsQuantity { get; set; }

    }
}
