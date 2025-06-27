
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
    public class HotelImageRepository : BaseRepository<HotelImage>, IHotelImageRepository
    {
        private readonly AppDbContext _context;
        public HotelImageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateRange(List<HotelImage> hotelImages)
        {
            await _context.AddRangeAsync(hotelImages);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HotelImage>> GetImagesByHotelId(int hotelId)
        {
            return await _context.HotelImages.Where(m=>m.HotelId == hotelId).ToListAsync();
        }

        public async Task UpdateRange(IEnumerable<HotelImage> hotelImages)
        {
             _context.HotelImages.UpdateRange(hotelImages);
            await _context.SaveChangesAsync();
        }
    }
}
