using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using Service.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        public BlogCategoryService(IBlogCategoryRepository blogCategoryRepository)
        {
            _blogCategoryRepository = blogCategoryRepository;
        }

        public async Task Create(BlogCategoryCreateVM model)
        {
            await _blogCategoryRepository.CreateAsync(new BlogCategory
            {
                Name = model.Name,
            });
        }

        public async Task Delete(int id)
        {
            var data = await _blogCategoryRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _blogCategoryRepository.DeleteAsync(data);
            }
        }

        public async Task Edit(int id, BlogCategoryEditVM model)
        {
            var data = await _blogCategoryRepository.GetById(id);
            if(data != null)
            {
                data.Id = id;
                data.Name = model.Name;
            }
            await _blogCategoryRepository.UpdateAsync(data);
        }

        public async Task<IEnumerable<BlogCategoryVM>> GetAllBlogCategories()
        {
            var datas = await _blogCategoryRepository.GetCategories();
            return datas.Select(m => new BlogCategoryVM
            {
                Name = m.Name,
                Id = m.Id,
                Count = m.Blogs.Count,
            });
        }

        public async Task<BlogCategoryVM> GetById(int id)
        {
            var data = await _blogCategoryRepository.GetById(id);
            return new BlogCategoryVM
            {
                Name = data.Name,
                Count = data.Blogs.Count,
                Id = data.Id
            };
        }
    }
}
