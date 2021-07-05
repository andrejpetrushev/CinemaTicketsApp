using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShopCinema.Web.Data;
using EShopCinema.Web.Models.Domain;
using EShopCinema.Web.Models.DTO;
using EShopCinema.Web.Models.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace EShopCinema.Web.Controllers
{
    public class CinemaTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CinemaTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CinemaTickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tickets.ToListAsync());
        }

        public async Task<IActionResult> AddTicketToCard(Guid? id)
        {
            var ticket = await _context.Tickets.Where(z => z.Id.Equals(id)).FirstOrDefaultAsync();
            AddToShoppingCardDto model = new AddToShoppingCardDto
            {
                SelectedTicket = ticket,
                TicketId = ticket.Id,
                Quantity = 1
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTicketToCard([Bind("CinemaTicketId", "Quantity")] AddToShoppingCardDto item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userShoppingCard = await _context.CinemaShoppingCarts.Where(z => z.CartOwnerId.Equals(userId)).FirstOrDefaultAsync();

            if (item.TicketId != null && userShoppingCard != null)
            {
                var ticket = await _context.Tickets.Where(z => z.Id.Equals(item.TicketId)).FirstOrDefaultAsync();

                if (ticket != null)
                {
                    CinemaTicketInShoppingCart itemToAdd = new CinemaTicketInShoppingCart
                    {
                        TicketCinema = ticket,
                        CinemaTicketId = ticket.Id,
                        CinemaShoppingCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        TicketsQuantity = item.Quantity
                    };

                    //var existingTicketItem = await _context.CinemaTicketInShoppingCarts.Where()

                    //if(existingTicketItem == null)
                    //{
                        _context.Add(itemToAdd);
                        await _context.SaveChangesAsync();
                    //}                    
                }
                return RedirectToAction("Index", "CinemaTickets");
            }

            return View(item);
        }

        // GET: CinemaTickets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketCinema = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,MovieFullName,MovieWatchType,MovieHall,RowNum,SeatNum,TicketMoviePrice,MovieStartTime,MovieENDTime,TicketUntilDate")] TicketCinema ticketCinema)
        {
            if (ModelState.IsValid)
            {
                ticketCinema.Id = Guid.NewGuid();
                _context.Add(ticketCinema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketCinema);
        }

        // GET: CinemaTickets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketCinema = await _context.Tickets.FindAsync(id);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,MovieFullName,MovieWatchType,MovieHall,RowNum,SeatNum,TicketMoviePrice,MovieStartTime,MovieENDTime,TicketUntilDate")] TicketCinema ticketCinema)
        {
            if (id != ticketCinema.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketCinema);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketCinema = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketCinema == null)
            {
                return NotFound();
            }

            return View(ticketCinema);
        }

        // POST: CinemaTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ticketCinema = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticketCinema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketCinemaExists(Guid id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
