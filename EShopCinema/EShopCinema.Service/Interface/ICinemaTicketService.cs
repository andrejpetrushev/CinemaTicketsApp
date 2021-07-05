using System;
using System.Collections.Generic;
using System.Text;
using EShopCinema.Domain.Domain;
using EShopCinema.Domain.DTO;

namespace EShopCinema.Services.Interface
{
    public interface ICinemaTicketService
    {
        List<TicketCinema> GetAllCinemaTickets();
        
        TicketCinema GetDetailsForCinemaTicket(Guid? id);

        void CreateNewTicket(TicketCinema tc);
        
        void UpdateExistingTicket(TicketCinema tc);
                
        AddToShoppingCardDto GetShoppingCartInfo(Guid? id);
        
        void DeleteTicket(Guid id);
        
        bool AddToShoppingCart(AddToShoppingCardDto item, string userId);

    }
}
