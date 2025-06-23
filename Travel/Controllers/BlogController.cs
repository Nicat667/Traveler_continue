using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModels.Blog;
using System.Threading.Tasks;

namespace Reservation_Final.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryService _blogCategoryService;
        public BlogController(IBlogService blogService, IBlogCategoryService blogCategoryService)
        {
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
        }
        public async Task<IActionResult> Index()
        {
            BlogAndCategoryVM blogAndCategoryVM = new BlogAndCategoryVM()
            {
                BlogCategories = await _blogCategoryService.GetAllBlogCategories(),
                Blogs = await _blogService.GetAllBlogsWithCategoriesAndImages()
            };
            return View(blogAndCategoryVM);
        }
        public async Task<IActionResult> BlogDetail(int id)
        {
            BlogAndCategoryVM blogAndCategoryVM = new BlogAndCategoryVM()
            {
                BlogCategories = await _blogCategoryService.GetAllBlogCategories(),
                Blog = await _blogService.GetBlogById(id)
            };
            return View(blogAndCategoryVM);
        }
        public async Task<IActionResult> BlogsByCategory(int id)
        {
            BlogAndCategoryVM blogAndCategoryVM = new BlogAndCategoryVM()
            {
                BlogCategories = await _blogCategoryService.GetAllBlogCategories(),
                Blogs = await _blogService.GetAllByCategoryId(id)
            };
            return View(blogAndCategoryVM);
        }
    }
}
