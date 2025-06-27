using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IHotelImageRepository : IBaseRepository<HotelImage>
    {
        Task CreateRange(List<HotelImage> hotelImages);
        Task<IEnumerable<HotelImage>> GetImagesByHotelId(int hotelId);
        Task UpdateRange(IEnumerable<HotelImage> hotelImages);
    }
}
