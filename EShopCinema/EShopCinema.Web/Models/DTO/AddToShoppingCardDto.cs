using EShopCinema.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopCinema.Web.Models.DTO
{
    public class AddToShoppingCardDto
    {
        public TicketCinema SelectedTicket { get; set; }
        public Guid TicketId { get; set; }
        public int Quantity { get; set; }

    }
}
