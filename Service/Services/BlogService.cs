using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using Service.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlobStorage _blobStorage;
        public BlogService(IBlogRepository blogRepository, IBlobStorage blobStorage)
        {
            _blogRepository = blogRepository;
            _blobStorage = blobStorage;
        }

        public async Task Create(BlogCreateVM model)
        {
            await _blogRepository.CreateAsync(new Blog
            {
                Image = await _blobStorage.UploadAsync(model.Image),
                AuthorImage = await _blobStorage.UploadAsync(model.AuthorImage),
                AuthorName = model.AuthorName,
                CategoryId = model.CategoryId,
                Content = model.Content,
                Title = model.Title,
                IsVisible = model.IsVisible,
                CreateDate = model.CreateDate,
            });
        }

        public async Task Delete(int id)
        {
            var data = await _blogRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _blogRepository.DeleteAsync(data);
                await _blobStorage.DeleteAsync(data.Image);
                //await _blobStorage.DeleteAsync(data.AuthorImage);   /////ATTENTION
            }
        }

        public async Task Edit(int id, BlogEditVM model)
        {
            var existData = await _blogRepository.GetByIdAsync(id);
            existData.AuthorName = model.AuthorName;
            existData.Title = model.Title;
            existData.CategoryId = model.CategoryId;
            existData.Content = model.Content;
            existData.IsVisible = model.IsVisible;
            if(model.Image != null)
            {
                existData.Image = await _blobStorage.ReplaceAsync(existData.Image, model.Image);
            }
            if (model.AuthorImage != null)
            {
                existData.Image = await _blobStorage.ReplaceAsync(existData.AuthorImage, model.AuthorImage);
            }
            await _blogRepository.UpdateAsync(existData);
        }

        public async Task<IEnumerable<BlogDetailVM>> GetAllBlogsWithCategoriesAndImages()
        {

            var datas = await _blogRepository.GetAllWithCategories();
            List<BlogDetailVM> blogs = new List<BlogDetailVM>();
            foreach (var data in datas)
            {
                BlogDetailVM blog = new BlogDetailVM()
                {
                    Id = data.Id,
                    Image = data.Image,
                    ImageUrl = await _blobStorage.GetBlobUrlAsync(data.Image),
                    AuthorImage = data.AuthorImage,
                    AuthorImageUrl = await _blobStorage.GetBlobUrlAsync(data.AuthorImage),
                    CategoryName = data.BlogCategory.Name,
                    Content = string.Join(" ", data.Content.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Take(44)) + " ...",
                    Title = data.Title,
                    AuthorName = data.AuthorName,
                    CreateDate = data.CreateDate.ToString("yyyy-MM-dd HH:mm"),
                };
                blogs.Add(blog);
            }
            return blogs;
        }

        public async Task<IEnumerable<BlogDetailVM>> GetAllByCategoryId(int id)
        {
            var datas = await _blogRepository.GetAllWithCategories();
            datas = datas.Where(m => m.CategoryId == id);
            List<BlogDetailVM> blogs = new List<BlogDetailVM>();
            foreach (var data in datas)
            {
                BlogDetailVM blog = new BlogDetailVM()
                {
                    Id = data.Id,
                    Image = data.Image,
                    ImageUrl = await _blobStorage.GetBlobUrlAsync(data.Image),
                    AuthorImage = data.AuthorImage,
                    AuthorImageUrl = await _blobStorage.GetBlobUrlAsync(data.AuthorImage),
                    CategoryName = data.BlogCategory.Name,
                    Content = string.Join(" ", data.Content.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Take(44)) + " ...",
                    Title = data.Title,
                    AuthorName = data.AuthorName,
                    CreateDate = data.CreateDate.ToString("yyyy-MM-dd HH:mm"),
                };
                blogs.Add(blog);
            }
            return blogs;
        }

        public async Task<IEnumerable<BlogVM>> GetAllWithCategories()
        {
            var datas = await _blogRepository.GetAllWithCategories();
            List<BlogVM> result = new List<BlogVM>();
            foreach (var item in datas)
            {
                BlogVM blogVM = new BlogVM()
                {
                    Id = item.Id,
                    Image = item.Image,
                    ImageUrl = await _blobStorage.GetBlobUrlAsync(item.Image),
                    Title = item.Title,
                    Content = string.Join(" ", item.Content.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Take(46)) + " ...",
                    CategoryName = item.BlogCategory.Name,
                    IsVisible = item.IsVisible
                };
                result.Add(blogVM);
            }
            return result;
        }

        public async Task<BlogDetailVM> GetBlogById(int id)
        {
            var result = await _blogRepository.GetBlogById(id);
            BlogDetailVM blog = new BlogDetailVM()
            {
                Id = result.Id,
                Image = result.Image,
                ImageUrl = await _blobStorage.GetBlobUrlAsync(result.Image),
                AuthorImage = result.AuthorImage,
                AuthorImageUrl = await _blobStorage.GetBlobUrlAsync(result.AuthorImage),
                AuthorName = result.AuthorName,
                Title = result.Title,
                Content = result.Content,
                CategoryName = result.BlogCategory.Name,
                CreateDate = result.CreateDate.ToString("yyyy-MM-dd HH:mm"),
                CategoryId = result.BlogCategory.Id,
                IsVisible = result.IsVisible
            };
            return blog;
        }
    }
}
