using Service.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogVM>> GetAllWithCategories();
        Task<IEnumerable<BlogDetailVM>> GetAllBlogsWithCategoriesAndImages();
        Task<IEnumerable<BlogDetailVM>> GetAllByCategoryId(int id);
        Task<BlogDetailVM> GetBlogById(int id);
    }
}
