using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Service.Services.Interfaces;
using Service.ViewModels.City;
using System.Threading.Tasks;

namespace Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _cityService.GetAllWihHotelsAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var data = await _cityService.GetById(id);
            if(data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityCreateVM model)
        {
            if(model == null)
            {
                return BadRequest(); 
            }
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            await _cityService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var existData = await _cityService.GetById(id);
            if(existData == null)
            {
                return NotFound();
            }
            return View(new CityEditVM
            {
                Name = existData.Name,
                ExistImage = existData.Image
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CityEditVM model)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var existData = await _cityService.GetById(id);
            if (existData == null)
            {
                return NotFound();
            }
            CityEditVM city = new CityEditVM()
            {
                Name = model.Name,
                Image = model.Image,
            };
            await _cityService.Edit(id, city);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var data = await _cityService.GetById(id);
            if(data == null)
            {
                return NotFound();
            }
            await _cityService.Delete(id);
            return Ok();
        }
    }
}
