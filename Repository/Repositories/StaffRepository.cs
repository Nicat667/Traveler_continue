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
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
    {
        private readonly AppDbContext _context;
        public StaffRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
