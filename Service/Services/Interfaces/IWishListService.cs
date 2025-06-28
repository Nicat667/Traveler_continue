using Service.ViewModels.WishListVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IWishListService
    {
        Task AddToSession(int hotelId);
        Task Create(WishListVM vm);

    }
}
