using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        private readonly AppDbContext _context;
        public BlogRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Blog>> GetAllWithCategories()
        {
            return await _context.Blogs.Include(m => m.BlogCategory).ToListAsync();
        }

    }
}
