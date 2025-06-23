using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IBlogCategoryRepository : IBaseRepository<BlogCategory>
    {
        Task<IEnumerable<BlogCategory>> GetCategories();
        Task<BlogCategory> GetById(int id);
    }
}
