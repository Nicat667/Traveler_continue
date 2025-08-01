﻿using Domain.Models;
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
    public class WishListRepository : BaseRepository<WishList>, IWishListRepository
    {
        private readonly AppDbContext _context;
        public WishListRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WishList>> GetByUserId(string userId)
        {
            return await _context.WishLists.Where(m=>m.AppUserId == userId).ToListAsync();
        }
    }
}
