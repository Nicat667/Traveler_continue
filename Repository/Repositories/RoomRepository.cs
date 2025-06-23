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
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        private readonly AppDbContext _context;
        public RoomRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _context.Rooms.Include(m=>m.RoomImages).ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAllRoomsWithReservationAndHotel()
        {
            return await _context.Rooms.Include(m=>m.Reservations).Include(m=>m.Hotel).ToListAsync();
        }

        public async Task<Room> GetRoomById(int id)
        {
            return await _context.Rooms.Include(m => m.RoomImages).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Room>> GetRoomsByHotelId(int hotelId)
        {
            return await _context.Rooms.Include(m=>m.RoomImages).Where(m=>m.HotelId == hotelId).ToListAsync();
        }
    }
}
