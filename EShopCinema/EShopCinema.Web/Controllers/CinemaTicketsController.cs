using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using EShopCinema.Web.Data;
//using EShopCinema.Web.Models.Domain;
//using EShopCinema.Web.Models.DTO;
//using EShopCinema.Web.Models.Identity;
using System.Security.Claims;
//using Microsoft.AspNetCore.Identity;
using EShopCinema.Services.Interface;
using EShopCinema.Domain.Domain;
using EShopCinema.Domain.DTO;


namespace EShopCinema.Web.Controllers
{
    public class CinemaTicketsController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly ICinemaTicketService _cinemaTicketService;

        public CinemaTicketsController(ICinemaTicketService cinemaTicketService)
        {
            //_context = context;
            _cinemaTicketService = cinemaTicketService;
        }

        // GET: CinemaTickets
        //public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            //return View(await _context.Tickets.ToListAsync());
            var allTickets = this._cinemaTicketService.GetAllTickets();
            return View(allTickets);
        }

        //public async Task<IActionResult> AddTicketToCard(Guid? id)
        public IActionResult AddTickettToCard(Guid? id)
        {
            //var ticket = await _context.Tickets.Where(z => z.Id.Equals(id)).FirstOrDefaultAsync();
            //AddToShoppingCardDto model = new AddToShoppingCardDto
            //{
            //    SelectedTicket = ticket,
            //    TicketId = ticket.Id,
            //    Quantity = 1
            //};
            var model = this._cinemaTicketService.GetShoppingCartInfo(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddTicketToCard([Bind("CinemaTicketId", "Quantity")] AddToShoppingCardDto item)
        public IActionResult AddTicketToCard([Bind("CinemaTicketId", "Quantity")] AddToShoppingCardDto item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var userShoppingCard = await _context.CinemaShoppingCarts.Where(z => z.CartOwnerId.Equals(userId)).FirstOrDefaultAsync();
            var result = this._cinemaTicketService.AddToShoppingCart(item, userId);

            //if (item.TicketId != null && userShoppingCard != null)
            if(result)
            {
                //var ticket = await _context.Tickets.Where(z => z.Id.Equals(item.TicketId)).FirstOrDefaultAsync();

                //if (ticket != null)
                //{
                //    CinemaTicketInShoppingCart itemToAdd = new CinemaTicketInShoppingCart
                //    {
                //        TicketCinema = ticket,
                //        CinemaTicketId = ticket.Id,
                //        CinemaShoppingCart = userShoppingCard,
                //        ShoppingCartId = userShoppingCard.Id,
                //        TicketsQuantity = item.Quantity
                //    };

                    //var existingTicketItem = await _context.CinemaTicketInShoppingCarts.Where()

                    //if(existingTicketItem == null)
                    //{
                        //_context.Add(itemToAdd);
                        //await _context.SaveChangesAsync();
                    //}                    
                }
                return RedirectToAction("Index", "CinemaTickets");
            }
            return View(item);
        }

        // GET: CinemaTickets/Details/5
        //public async Task<IActionResult> Details(Guid? id)
         public IActionResult Details(Guid? id)
         {
            if (id == null)
            {
                return NotFound();
            }

           //var ticketCinema = await _context.Tickets
           //    .FirstOrDefaultAsync(m => m.Id == id);
           var ticketCinema = this._cinemaTicketService.GetDetailsForCinemaTicket(id);
           if (ticketCinema == null)
           {
                return NotFound();
           }

            return View(ticketCinema);
         }

        // GET: CinemaTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CinemaTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,MovieFullName,MovieWatchType,MovieHall,RowNum,SeatNum,TicketMoviePrice,MovieStartTime,MovieENDTime,TicketUntilDate")] TicketCinema ticketCinema)
        {
            if (ModelState.IsValid)
            {
                //ticketCinema.Id = Guid.NewGuid();
                //_context.Add(ticketCinema);
                //await _context.SaveChangesAsync();
                this._cinemaTicketService.CreateNewTicket(ticketCinema);
                return RedirectToAction(nameof(Index));
            }
            return View(ticketCinema);
        }

        // GET: CinemaTickets/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var ticketCinema = await _context.Tickets.FindAsync(id);
            var ticketCinema = this._cinemaTicketService.GetDetailsForTicket(id);
            
            if (ticketCinema == null)
            {
                return NotFound();
            }
            return View(ticketCinema);
        }

        // POST: CinemaTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,MovieFullName,MovieWatchType,MovieHall,RowNum,SeatNum,TicketMoviePrice,MovieStartTime,MovieENDTime,TicketUntilDate")] TicketCinema ticketCinema)
        {
            if (id != ticketCinema.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(ticketCinema);
                    //await _context.SaveChangesAsync();
                    this._cinemaTicketService.UpdateExistingTicket(ticketCinema);
            }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketCinemaExists(ticketCinema.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticketCinema);
        }

        // GET: CinemaTickets/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var ticketCinema = await _context.Tickets
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var ticketCinema = this._cinemaTicketService.GetDetailsForTicket(id);
            if (ticketCinema == null)
            {
                return NotFound();
            }

            return View(ticketCinema);
        }

        // POST: CinemaTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            //var ticketCinema = await _context.Tickets.FindAsync(id);
            //_context.Tickets.Remove(ticketCinema);
            //await _context.SaveChangesAsync();
            this._cinemaTicketService.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TicketCinemaExists(Guid id)
        {
            //return _context.Tickets.Any(e => e.Id == id);
            return this._cinemaTicketService.GetDetailsForCinemaTicket(id) != null;
        }
}

