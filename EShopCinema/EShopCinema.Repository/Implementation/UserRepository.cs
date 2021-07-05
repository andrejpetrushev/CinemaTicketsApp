using EShopCinema.Domain.Identity;
using EShopCinema.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShopCinema.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<EShopApplicationCinemaUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<EShopApplicationCinemaUser>();
        }
        public IEnumerable<EShopApplicationCinemaUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public EShopApplicationCinemaUser Get(string id)
        {
            return entities
               .Include(z => z.UserCart)
               .Include("UserCart.CinemaTicketInShoppingCarts")
               .Include("UserCart.CinemaTicketInShoppingCarts.TicketCinema")
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(EShopApplicationCinemaUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(EShopApplicationCinemaUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(EShopApplicationCinemaUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                entities.Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
