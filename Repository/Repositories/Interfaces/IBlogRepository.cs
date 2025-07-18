﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IBlogRepository : IBaseRepository<Blog>
    {
        Task<IEnumerable<Blog>> GetAllWithCategories();
        Task<Blog> GetBlogById(int id);
    }
}
