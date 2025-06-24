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
    public class EmailRepository : BaseRepository<Email>, IEmailRepository
    {
        private readonly AppDbContext _context;
        public EmailRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
