using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services.Interfaces;
using Service.ViewModels.Blog;
using System.Threading.Tasks;

namespace Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryService _blogCategoryService;
        public BlogController(IBlogService blogService, IBlogCategoryService blogCategoryService)
        {
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetAllWithCategories());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var data = await _blogService.GetBlogById(id);
            if(data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var data = await _blogService.GetBlogById(id);
            if(data == null)
            {
                return NotFound();
            }
            await _blogService.Delete(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _blogCategoryService.GetAllBlogCategories();
            ViewBag.Categories = categories.Select(m=> new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM model)
        {
            var categories = await _blogCategoryService.GetAllBlogCategories();
            ViewBag.Categories = categories.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }).ToList();
            if (model == null)
            {
                return BadRequest();
            }
            model.CreateDate = DateTime.Now;
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            await _blogService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var categories = await _blogCategoryService.GetAllBlogCategories();
            ViewBag.Categories = categories.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }).ToList();
            if (id ==  null)
            {
                return BadRequest();
            }
            var existData = await _blogService.GetBlogById(id);
            if (existData == null)
            {
                return NotFound();
            }
            BlogEditVM blog = new BlogEditVM()
            {
                Title = existData.Title,
                ExistAuthorImage = existData.AuthorImageUrl,
                AuthorName = existData.AuthorName,
                ExistImage = existData.ImageUrl,
                Content = existData.Content,
                IsVisible = existData.IsVisible,
                CategoryId = existData.CategoryId,
            };
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  BlogEditVM model)
        {
            var categories = await _blogCategoryService.GetAllBlogCategories();
            ViewBag.Categories = categories.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }).ToList();
            if (id == null)
            {
                return BadRequest();
            }
            var existData = await _blogService.GetBlogById(id);
            if (existData == null)
            {
                return NotFound();
            }
            await _blogService.Edit(id, model);
            return RedirectToAction("Index");
        }
    }
}
