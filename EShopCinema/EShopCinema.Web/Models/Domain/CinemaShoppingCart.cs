using EShopCinema.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopCinema.Web.Models.Domain
{
    public class CinemaShoppingCart
    {
        public Guid Id { get; set; }
        public string CartOwnerId { get; set; }
        public virtual EShopApplicationCinemaUser CinemaCartOwner { get; set; }
        public virtual ICollection<CinemaTicketInShoppingCart> Tickets { get; set; }
        public virtual ICollection<CinemaTicketInShoppingCart> CinemaTicketInShoppingCarts { get; set; }

        public CinemaShoppingCart()
        {

        }


    }
}
