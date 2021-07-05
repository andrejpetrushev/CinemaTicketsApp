using EShopCinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopCinema.Domain.Domain
{
    public class Order : BaseEntity
    {
        //public Guid Id { get; set; }
        public string UserId { get; set; }
        public EShopApplicationCinemaUser User { get; set; }
        public IEnumerable<CinemaTicketInOrder> CinemaTicketInOrders { get; set; }
    }
}
