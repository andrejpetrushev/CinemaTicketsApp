using EShopCinema.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopCinema.Domain.DTO
{
    public class AddToShoppingCardDto
    {
        public TicketCinema SelectedTicket { get; set; }
        public Guid TicketId { get; set; }
        public int Quantity { get; set; }

    }
}
