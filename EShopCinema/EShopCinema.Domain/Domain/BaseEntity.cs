using System;
using System.Collections.Generic;
using System.Text;

namespace EShopCinema.Domain.Domain
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

    }
}
