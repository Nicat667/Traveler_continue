using Domain.Models;

namespace Repository.Repositories.Interfaces
{
    public interface IHotelRepository : IBaseRepository<Hotel>
    {
        Task<IEnumerable<Hotel>> GetAllHotel();
        Task<Hotel> GetHotelById(int id);
        Task<IEnumerable<Hotel>> GetAllPaginated(int page, int take);
        Task<IEnumerable<Hotel>> Search();
    }
}
