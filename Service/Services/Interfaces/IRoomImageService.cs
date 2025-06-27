using Service.ViewModels.HotelImages;
using Service.ViewModels.Room;
using Service.ViewModels.RoomImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IRoomImageService
    {
        Task Create(RoomImageCreateVM model);
        Task AddImages(AddRoomImageVM model);
        Task Delete(int id);
        Task SetMain(int id, int roomId);
        Task Change(int id, ChangeRoomImageVM model);
        Task<RoomImageVM> GetById(int id);
    }
}
