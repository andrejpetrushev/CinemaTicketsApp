using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShopCinema.Web.Models.Domain
{
    public class TicketCinema
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

        public virtual ICollection<CinemaTicketInShoppingCart> CinemaShoppingCarts { get; set; }

        public TicketCinema()
        {

        }
    }
}
