using EShopCinema.Domain.Domain;
using EShopCinema.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopCinema.Repository
{
    public class ApplicationDbContext : IdentityDbContext<EShopApplicationCinemaUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TicketCinema> Tickets { get; set; }
        public virtual DbSet<CinemaShoppingCart> CinemaShoppingCarts { get; set; }
        public virtual DbSet<CinemaTicketInShoppingCart> CinemaTicketInShoppingCarts { get; set; }
        public virtual DbSet<CinemaTicketInOrder> CinemaTicketsInOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TicketCinema>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<CinemaShoppingCart>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<CinemaTicketInShoppingCart>()
                .HasKey(ctisc => new { ctisc.CinemaTicketId, ctisc.ShoppingCartId });

            builder.Entity<CinemaTicketInShoppingCart>()
                .HasOne(z => z.TicketCinema)
                .WithMany(t => t.CinemaShoppingCarts)
                .HasForeignKey(z => z.CinemaTicketId);

            builder.Entity<CinemaTicketInShoppingCart>()
                .HasOne(z => z.CinemaShoppingCart)
                .WithMany(t => t.Tickets)
                .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<CinemaShoppingCart>()
                .HasOne<EShopApplicationCinemaUser>(z => z.CinemaCartOwner)
                .WithOne(sc => sc.CinemaCart)
                .HasForeignKey<CinemaShoppingCart>(bt => bt.CartOwnerId);

            builder.Entity<CinemaTicketInOrder>()
               .HasKey(z => new { z.CinemaTicketId, z.OrderId });

            builder.Entity<CinemaTicketInOrder>()
                .HasOne(z => z.OrderedCinemaTicket)
                .WithMany(z => z.CinemaTicketInOrders)
                .HasForeignKey(z => z.OrderId);

            builder.Entity<CinemaTicketInOrder>()
                .HasOne(z => z.UserOrder)
                .WithMany(z => z.CinemaTicketInOrders)
                .HasForeignKey(z => z.CinemaTicketId);
        }
    }
}
