using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EShopCinema.Domain.Domain
{
    public class TicketCinema : BaseEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string MovieFullName { get; set; }
        [Required]
        public string MovieWatchType { get; set; }
        [Required]
        public int MovieHall { get; set; }
        [Required]
        public int RowNum { get; set; }
        [Required]
        public int SeatNum { get; set; }
        [Required]
        public int TicketMoviePrice { get; set; }
        [Required]
        public DateTime MovieStartTime { get; set; }
        [Required]
        public DateTime MovieENDTime { get; set; }
        [Required]
        public DateTime TicketUntilDate { get; set; }
        public string genreMovie { get; set; }

        public virtual ICollection<CinemaTicketInShoppingCart> CinemaShoppingCarts { get; set; }

        //public virtual ICollection<CinemaTicketInOrder> CinemaTicketInOrders { get; set; }

        public IEnumerable<CinemaTicketInOrder> CinemaTicketInOrders { get; set; }

        public TicketCinema()
        {

        }
    }
}
