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
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        private readonly AppDbContext _context;
        public CityRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<City>> GetAllWithHotels()
        {
            return await _context.City.Include(m => m.Hotels).ToListAsync();
        }

        public async Task<City> GetByIdWithHotel(int id)
        {
            return await _context.City.Include(m => m.Hotels).FirstOrDefaultAsync(m=>m.Id == id);
        }
    }
}
