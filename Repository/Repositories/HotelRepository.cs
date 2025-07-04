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
    public class HotelRepository : BaseRepository<Hotel>, IHotelRepository
    {
        private readonly AppDbContext _context;
        public HotelRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotel>> GetAllHotel()
        {
            return await _context.Hotels.Include(m=>m.Rooms).ThenInclude(m=>m.RoomImages).Include(m=>m.Comments).Include(m=>m.HotelImages).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetAllPaginated(int page, int take)
        {
            return await _context.Hotels.Include(m => m.Rooms).ThenInclude(mx => mx.RoomImages).Include(m => m.Comments).Include(m => m.HotelImages).Skip((page * take)-1).Take(take).ToListAsync();
        }

        public async Task<Hotel> GetHotelById(int id)
        {
            return await _context.Hotels.Include(m => m.Rooms).ThenInclude(mx => mx.RoomImages).Include(m => m.Comments).Include(m => m.HotelImages).Include(m=>m.City).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Hotel>> Search()
        {
            return await _context.Hotels.Include(m => m.Rooms).ThenInclude(m => m.Reservations).Include(m => m.Rooms).ThenInclude(m => m.RoomImages).Include(m => m.Comments).Include(m => m.HotelImages).Include(m=>m.City).ToListAsync();
        }
    }
}
