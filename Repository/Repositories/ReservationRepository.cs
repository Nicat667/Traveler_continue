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
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        private readonly AppDbContext _context;
        public ReservationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
