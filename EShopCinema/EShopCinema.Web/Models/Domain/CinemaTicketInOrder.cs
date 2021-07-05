using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopCinema.Web.Models.Domain
{
    public class CinemaTicketInOrder
    {
        public Guid CinemaTicketId { get; set; }
        public TicketCinema OrderedCinemaTicket { get; set; }

        public Guid OrderId { get; set; }
        public Order UserOrder { get; set; }
    }
}
