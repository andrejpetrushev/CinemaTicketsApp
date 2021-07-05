using EShopCinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopCinema.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<EShopApplicationCinemaUser> GetAll();
        EShopApplicationCinemaUser Get(string id);
        void Insert(EShopApplicationCinemaUser entity);
        void Update(EShopApplicationCinemaUser entity);
        void Delete(EShopApplicationCinemaUser entity);
    }
}
