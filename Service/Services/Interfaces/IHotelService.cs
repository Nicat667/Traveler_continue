using Service.Helpers.Responses;
using Service.ViewModels;
using Service.ViewModels.Comments;
using Service.ViewModels.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelVM>> GetAllHotel();
        Task<HotelDetailVM> GetHotelDetail(int id);
        Task<PaginateResponse<HotelVM>> GetAllHotelsPaginated(int page, int take=6);
        Task<IEnumerable<HotelVM>> HotelFilter(FilterVM filter);
        Task<IEnumerable<HotelVM>> HotelFilterByCity(int id);
        Task<IEnumerable<HotelVM>> Search(SearchVM query);
        
    }
}
