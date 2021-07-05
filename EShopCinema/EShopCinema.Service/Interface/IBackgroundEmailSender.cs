using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShopinema.Services.Interface
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}
