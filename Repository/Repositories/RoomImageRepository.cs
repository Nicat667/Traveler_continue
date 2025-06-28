using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class RoomImageRepository : BaseRepository<RoomImage>, IRoomImageRepository
    {
        private readonly AppDbContext _context;
        public RoomImageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomImage>> GetAllByRoomId(int roomId)
        {
            return await _context.RoomImages.Where(m=>m.RoomId == roomId).ToListAsync();
        }

        public async Task<IEnumerable<RoomImage>> GetRoomImagesByRoomId(int id)
        {
            return await _context.RoomImages.Where(m=>m.RoomId == id).ToListAsync();
        }

        public async Task UpdateRange(IEnumerable<RoomImage> models)
        {
            foreach (var model in models)
            {
                var existData = await _context.RoomImages.FirstOrDefaultAsync(m=>m.Id == model.Id);
                existData.Name = model.Name;
                existData.IsMain = model.IsMain;
                existData.RoomId = model.RoomId;
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
        }
    }
}
