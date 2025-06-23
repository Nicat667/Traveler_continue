using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        Task<IEnumerable<Room>> GetAllRooms();
        Task<IEnumerable<Room>> GetRoomsByHotelId(int hotelId);
        Task<Room> GetRoomById(int id);
        Task<IEnumerable<Room>> GetAllRoomsWithReservationAndHotel();
    }
}
