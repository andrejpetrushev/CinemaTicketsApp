using EShopCinema.Domain.Domain;
using EShopCinema.Domain.DTO;
using EShopCinema.Repository.Interface;
using EShopCinema.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShopCinema.Services.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<CinemaTicketInOrder> _cinemaTicketInOrderRepository;
        private readonly IUserRepository _userRepository;

        //public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<CinemaTicketInOrder> cinemaTicketInOrderRepository, IRepository<Order> orderRepository, IUserRepository userRepository)
        public ShoppingCartService(IRepository<EmailMessage> mailRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<ProductInOrder> productInOrderRepositorty, IRepository<Order> orderRepositorty, IUserRepository userRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _cinemaTicketInOrderRepositorty = cinemaTicketInOrderRepository;
            _mailRepository = mailRepository;
        }

        public bool deleteCinemaTicketFromShoppingCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.CinemaTicketInShoppingCarts.Where(z => z.CinemaTicketId.Equals(id)).FirstOrDefault();

                userShoppingCart.CinemaTicketInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }

            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userShoppingCart = loggedInUser.UserCart;

            var AllTickets = userShoppingCart.CinemaTicketInShoppingCarts.ToList();

            var allTicketsPrice = AllTickets.Select(z => new
            {
                TicketPrice = z.TicketCinema.TicketMoviePrice,
                TicietsQuantity = z.Quantity
            }).ToList();

            var totalPrice = 0;


            foreach (var item in allTicketsPrice)
            {
                totalPrice += item.TicketsQuantity * item.TicketMoviePrice;
            }


            ShoppingCartDto scDto = new ShoppingCartDto
            {
                CinemaTickets = AllTickets,
                TotalPrice = totalPrice
            };


            return scDto;

        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                EmailMessage mail = new EmailMessage();
                mail.MailTo = loggedInUser.Email;
                mail.Subject = "Successfully created order";
                mail.Status = false;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<CinemaTicketInOrder> cinemaTicketInOrders = new List<CinemaTicketInOrder>();

                var result = userShoppingCart.CinemaTicketInShoppingCarts.Select(z => new CinemaTicketInOrder
                {
                    Id = Guid.NewGuid(),
                    CinemaTicketId = z.TicketCinema.Id,
                    OrderedCinemaTicket = z.TicketCinema,
                    OrderId = order.Id,
                    UserOrder = order
                }).ToList();

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0;

                sb.AppendLine("Your order is completed. The order conains: ");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i];

                    totalPrice += item.Quantity * item.OrderedCinemaTicket.TicketMoviePrice;

                    sb.AppendLine(i.ToString() + ". " + item.OrderedCinemaTicket.TicketMovieFullName + " with price of: " + item.OrderedCinemaTicket.TicketMoviePrice + " and quantity of: " + item.TicketsQuantity);
                }

                sb.AppendLine("Total price: " + totalPrice.ToString());


                mail.Content = sb.ToString();

                cinemaTicketInOrders.AddRange(result);

                foreach (var item in cinemaTicketInOrders)
                {
                    this._cinemaTicketInOrderRepositorty.Insert(item);
                }

                loggedInUser.UserCart.CinemaTicketInShoppingCarts.Clear();

                this._userRepository.Update(loggedInUser);
                this._mailRepository.Insert(mail);

                return true;
            }
            return false;
        }
    }
}