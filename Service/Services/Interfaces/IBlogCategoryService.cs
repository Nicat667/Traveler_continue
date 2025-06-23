using Service.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IBlogCategoryService
    {
        Task<IEnumerable<BlogCategoryVM>> GetAllBlogCategories();
        Task Create(BlogCategoryCreateVM model);
        Task<BlogCategoryVM> GetById(int id);
        Task Delete(int id);
        Task Edit(int id, BlogCategoryEditVM model);
    }
}
