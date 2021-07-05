/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EShopCinema.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EShopCinema.Web.Models.Domain;
using EShopCinema.Web.Models.DTO;
using EShopCinema.Web.Models.Identity;*/

using EShopCinema.Domain.Domain;
using EShopCinema.Domain.DTO;
using EShopCinema.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

/*namespace EShopCinema.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //Select * from Users Where Id LIKE userId

            var loggedInUser = await _context.Users.Where(z => z.Id == userId)
                .Include("UserCart")
                .Include("UserCart.CinemaTicketInShoppingCarts")
                .Include("UserCart.CinemaTicketInShoppingCarts.TicketCinema")
                .FirstOrDefaultAsync();

            var userShoppingCart = loggedInUser.UserCart;

            var AllTickets = userShoppingCart.Tickets.ToList();

            var allTicketsPrice = AllTickets.Select(z => new
            {
                TicketPrice = z.TicketCinema.TicketMoviePrice,
                TicketsQuantity = z.TicketsQuantity
            }).ToList();

            var totalPrice = 0;


            foreach (var item in allTicketsPrice)
            {
                totalPrice += item.TicketsQuantity * item.TicketPrice;
            }


            ShoppingCartDto scDto = new ShoppingCartDto
            {
                CinemaTickets = AllTickets,
                TotalPrice = totalPrice
            };

            return View(scDto);
        }

        public async Task<IActionResult> DeleteFromShoppingCart(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = await _context.Users.Where(z => z.Id == userId)
                    .Include("UserCart")
                    .Include("UserCart.CinemaTicketInShoppingCarts")
                    .Include("UserCart.CinemaTicketInShoppingCarts.TicketCinema")
                    .FirstOrDefaultAsync();

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.CinemaTicketInShoppingCarts.Where(z => z.CinemaTicketId.Equals(id)).FirstOrDefault();

                userShoppingCart.CinemaTicketInShoppingCarts.Remove(itemToDelete);

                _context.Update(userShoppingCart);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "ShoppingCart");
        }

        public async Task<IActionResult> Order()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userId))
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = await _context.Users.Where(z => z.Id == userId)
                    .Include("UserCart")
                    .Include("UserCart.CinemaTicketInShoppingCarts")
                    .Include("UserCart.CinemaTicketInShoppingCarts.TicketCinema")
                    .FirstOrDefaultAsync();

                var userShoppingCart = loggedInUser.UserCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                _context.Add(order);
                await _context.SaveChangesAsync();

                List<CinemaTicketInOrder> cinemaTicketInOrders = new List<CinemaTicketInOrder>();

                var result = userShoppingCart.CinemaTicketInShoppingCarts.Select(z => new CinemaTicketInOrder
                {
                    CinemaTicketId = z.TicketCinema.Id,
                    OrderedCinemaTicket = z.TicketCinema,
                    OrderId = order.Id,
                    UserOrder = order
                }).ToList();

                cinemaTicketInOrders.AddRange(result);

                foreach (var item in cinemaTicketInOrders)
                {
                    _context.Add(item);
                }
                await _context.SaveChangesAsync();

                loggedInUser.UserCart.CinemaTicketInShoppingCarts.Clear();

                _context.Update(loggedInUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}*/


namespace EShopCinema.Web.Controllers
{
    public class ShoppingCartController : Controller
    {


        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._shoppingCartService.getShoppingCartInfo(userId));
        }

        public IActionResult DeleteFromShoppingCart(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.deleteCinemaTicketFromShoppingCart(userId, id);

            if (result)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
        }

        public IActionResult Order()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.orderNow(userId);

            if (result)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
        }
    }
}
