using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class FAQRepository : BaseRepository<FAQ>, IFAQRepository
    {
        private readonly AppDbContext _context;
        public FAQRepository(AppDbContext context) : base(context)
        {

        }
    }
}
