using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EShopCinema.Domain.Domain;
using EShopCinema.Domain.DTO;
using EShopCinema.Repository.Interface;
using EShopCinema.Services.Interface;


namespace EShopCinema.Services.Implementation
{
    public class CinemaTicketService : ICinemaTicketService
    {
        private readonly IRepository<TicketCinema> _cinemaTicketRepository;
        private readonly IRepository<CinemaTicketInShoppingCart> _cinemaTicketInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        public CinemaTicketService(IRepository<TicketCinema> cinemaTicketRepository, IRepository<CinemaTicketInShoppingCart> cinemaTicketInShoppingCartRepository, IUserRepository userRepository)
        {
            _cinemaTicketRepository = cinemaTicketRepository;
            _userRepository = userRepository;
            _cinemaTicketInShoppingCartRepository = cinemaTicketInShoppingCartRepository;
        }

        public bool AddToShoppingCart(AddToShoppingCardDto item, string userId)
        {

            var user = this._userRepository.Get(userId);

            var userShoppingCard = user.UserCart;

            if (item.CinemaTicketId != null && userShoppingCard != null)
            {
                var cinemaTicket = this.GetDetailsForCinemaTicket(item.CinemaTicketId);

                if (cinemaTicket != null)
                {
                    CinemaTicketInShoppingCart itemToAdd = new CinemaTicketInShoppingCart
                    {
                        TicketCinema = cinemaTicket,
                        CinemaTicketId = cinemaTicket.Id,
                        ShoppingCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        TicketsQuantity = item.Quantity
                    };

                    this._cinemaTicketInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewTicket(TicketCinema tc)
        {
            this._cinemaTicketRepository.Insert(tc);
        }

        public void DeleteTicket(Guid id)
        {
            var cinemaTicket = this.GetDetailsForCinemaTicket(id);
            this._cinemaTicketRepository.Delete(cinemaTicket);
        }

        public List<TicketCinema> GetAllTickets()
        {
            return this._cinemaTicketRepository.GetAll().ToList();
        }

        public TicketCinema GetDetailsForCinemaTicket(Guid? id)
        {
            return this._cinemaTicketRepository.Get(id);
        }

        public AddToShoppingCardDto GetShoppingCartInfo(Guid? id)
        {
            var cinemaTicket = this.GetDetailsForCinemaTicket(id);
            AddToShoppingCardDto model = new AddToShoppingCardDto
            {
                SelectedTicket = cinemaTicket,
                CinemaTicketId = cinemaTicket.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdateExistingTicket(TicketCinema tc)
        {
            this._cinemaTicketRepository.Update(tc);
        }
    }
}
