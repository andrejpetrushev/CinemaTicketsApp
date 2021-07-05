using EShopCinema.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopCinema.Services.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteCinemaTicketFromShoppingCart(string userId, Guid id);
        bool orderNow(string userId);
    }
}