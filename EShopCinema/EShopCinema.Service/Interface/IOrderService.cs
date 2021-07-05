using EShopCinema.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopCinema.Services.Interface
{
    public interface IOrderService
    {
        List<Order> getAllOrders();

        Order getOrderDetails(BaseEntity model);
    }
}