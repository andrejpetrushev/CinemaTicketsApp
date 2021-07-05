using EShopCinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopCinema.Domain.Domain
{
    public class CinemaShoppingCart : BaseEntity
    {
        //public Guid Id { get; set; }
        public string CartOwnerId { get; set; }
        public virtual EShopApplicationCinemaUser CinemaCartOwner { get; set; }

        //public virtual ICollection<CinemaTicketInShoppingCart> Tickets { get; set; }
        public virtual ICollection<CinemaTicketInShoppingCart> CinemaTicketInShoppingCarts { get; set; }

        //public CinemaShoppingCart()
        //{

        //}
    }
}
