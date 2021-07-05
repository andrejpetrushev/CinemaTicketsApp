using System;
using System.Collections.Generic;
using System.Text;

namespace EShopCinema.Domain.Domain
{
    public class CinemaTicketInOrder : BaseEntity
    {
        public Guid CinemaTicketId { get; set; }
        public TicketCinema OrderedCinemaTicket { get; set; }

        public Guid OrderId { get; set; }
        public Order UserOrder { get; set; }
        public int Quantity { get; set; }
    }
}
