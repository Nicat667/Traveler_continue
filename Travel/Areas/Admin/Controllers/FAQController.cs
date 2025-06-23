using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModels.FAQ;
using System.Threading.Tasks;

namespace Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FAQController : Controller
    {
        private readonly IFAQService _faqService;
        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _faqService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVM model)
        {
            if(model == null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            await _faqService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var data = await _faqService.GetById(id);
            if(data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var existData = await _faqService.GetById(id);
            if(existData == null)
            {
                return NotFound();
            }
            return View(new EditVM { Answer = existData.Answer, Question = existData.Question});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditVM model)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var existData = await _faqService.GetById(id);
            if (existData == null)
            {
                return NotFound();
            }
            await _faqService.Edit(id, model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var existData = await _faqService.GetById(id);
            if (existData == null)
            {
                return NotFound();
            }
            await _faqService.Delete(id);
            return Ok();
        }
    }
}
