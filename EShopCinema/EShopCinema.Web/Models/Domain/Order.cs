using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopCinema.Web.Models.Identity;

namespace EShopCinema.Web.Models.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public EShopApplicationCinemaUser User { get; set; }
        public IEnumerable<CinemaTicketInOrder> CinemaTicketInOrders { get; set; }
    }
}
