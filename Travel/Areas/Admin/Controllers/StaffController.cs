using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModels.Staff;
using System.Threading.Tasks;

namespace Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _staffService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var data = await _staffService.GetById(id);
            if(data == null)
            {
                return NotFound();
            }
            return View(new StaffVM { Name = data.Name, Position = data.Position, ImageUrl = data.ImageUrl});
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StaffCreateVM model)
        {
            if(model == null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            await _staffService.Create(model);
            return RedirectToAction("Index");   
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var data = await _staffService.GetById(id);
            if (data == null)
            {
                return NotFound();
            }
            await _staffService.Delete(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var existData = await _staffService.GetById(id);
            if(existData == null)
            {
                return NotFound();
            }
            return View(new StaffEditVM
            {
                Name = existData.Name,
                Position = existData.Position,
                ExistImage = existData.ImageUrl,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StaffEditVM model)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var existData = await _staffService.GetById(id);
            if (existData == null)
            {
                return NotFound();
            }
            await _staffService.Update(id, model);
            return RedirectToAction("Index");
        }
    }
}
