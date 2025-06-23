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
    public class SettingRepository : BaseRepository<Setting>, ISettingRepository
    {
        private readonly AppDbContext _context;
        public SettingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Setting>> GetAllSettingAsync()
        {
            return await _context.Settings.ToListAsync();
        }

        public async Task<Dictionary<string, string>> GetSettingAsync()
        {
            return await _context.Settings.ToDictionaryAsync(m=>m.Key, m=>m.Value);
        }


    }
}
