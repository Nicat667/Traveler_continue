using Service.Helpers.Responses;
using Service.ViewModels.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomVM>> GetAllRooms();
        Task<List<RoomVM>> GetRoomsByHotelId(int hotelId);
        Task<RoomDetailVM> GetRoomById(int id);
        Task<bool> BookRoom(BookVM model);
        Task Create(RoomCreateVM model);   
        Task<IEnumerable<RoomVM>> GetRoomsByHotelIdWithoutGrouping(int hotelId);
        Task Delete(int id);
        Task Edit(int id, RoomEditVM model);
    }
}
