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
    }
}
