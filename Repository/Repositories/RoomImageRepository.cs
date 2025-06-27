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
    public class RoomImageRepository : BaseRepository<RoomImage>, IRoomImageRepository
    {
        private readonly AppDbContext _context;
        public RoomImageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
