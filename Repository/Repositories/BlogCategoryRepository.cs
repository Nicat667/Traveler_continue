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
    public class BlogCategoryRepository : BaseRepository<BlogCategory>, IBlogCategoryRepository
    {
        private readonly AppDbContext _context;
        public BlogCategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BlogCategory> GetById(int id)
        {
            return await _context.BlogCategories.Include(m => m.Blogs).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<BlogCategory>> GetCategories()
        {
            return await _context.BlogCategories.Include(m => m.Blogs).ToListAsync();
        }
    }
}
