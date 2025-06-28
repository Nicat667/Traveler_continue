using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IRoomImageRepository : IBaseRepository<RoomImage>
    {
        Task<IEnumerable<RoomImage>> GetRoomImagesByRoomId(int id);
        Task UpdateRange(IEnumerable<RoomImage> models);
        Task<IEnumerable<RoomImage>> GetAllByRoomId(int roomId);
    }
}
