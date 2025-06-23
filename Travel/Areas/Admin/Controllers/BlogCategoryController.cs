using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModels.Blog;
using System.Threading.Tasks;

namespace Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogCategoryController : Controller
    {
        private readonly IBlogCategoryService _blogCategoryService;
        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _blogCategoryService.GetAllBlogCategories());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var data = await _blogCategoryService.GetById(id);
            if(data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var existData = await _blogCategoryService.GetById(id);
            if (existData == null)
            {
                return NotFound();
            }

            return View(new BlogCategoryEditVM
            {
                Name = existData.Name
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogCategoryEditVM model)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var existData = await _blogCategoryService.GetById(id);
            if (existData == null)
            {
                return NotFound();
            }
            await _blogCategoryService.Edit(id, model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCategoryCreateVM model)
        {
            if(model == null)
            {
                return BadRequest();
            }
            await _blogCategoryService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            await _blogCategoryService.Delete(id);
            return Ok();
        }
    }
}
