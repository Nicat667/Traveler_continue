using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;
using Service.Services.Interfaces;
using Service.ViewModels.Hotel;
using Service.ViewModels.HotelImages;
using System.Threading.Tasks;

namespace Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly ICityService _cityService;
        private readonly IHotelImageService _hotelImageService;
        public HotelController(IHotelService hotelService, ICityService cityService, IHotelImageService hotelImageService)
        {
            _hotelService = hotelService;
            _cityService = cityService;
            _hotelImageService = hotelImageService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _hotelService.GetAllHotel());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var data = await _hotelService.GetHotelDetail(id);
            if(data == null)
            {
                return NotFound();
            }
            return View(data);  
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var cities = await _cityService.GetAll();
            ViewBag.Cities = cities.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HotelCreateVM model)
        {
            var cities = await _cityService.GetAll();
            ViewBag.Cities = cities.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }).ToList();
            if (model == null)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return View(model);
            }

            if(model.Images.Count() < 5)
            {
                ModelState.AddModelError("Images", "Add at least 5 images!");
                return View(model);
            }
            await _hotelService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var data = await _hotelService.GetHotelDetail(id);
            if (data == null)
            {
                return NotFound();
            }
            await _hotelService.Delete(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cities = await _cityService.GetAll();
            ViewBag.Cities = cities.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }).ToList();
            if (id == null)
            {
                return BadRequest();
            }
            var existData = await _hotelService.GetHotelDetail(id);
            if (existData == null)
            {
                return NotFound();
            }
            HotelEditVM hotelEditVM = new HotelEditVM()
            {
                Id = existData.Id,
                Name = existData.Name,
                Address = existData.Address,
                Description = existData.Description,
                StarCount = existData.StarCount,
                SpaSauna = existData.SpaSauna,
                Restaurant = existData.Restaurant,
                AirConditioning = existData.AirConditioning,
                AirportTransport = existData.AirportTransport,
                CityId = existData.Id,
                FitnessCenter = existData.FitnessCenter,
                Parking = existData.Parking,

                ExistImages = existData.ImageUrls.Select(m=>new HotelImageVM
                {
                    HotelId = m.Id,
                    Id = m.Id,
                    IsMain = m.IsMain,
                    Url = m.Url
                })
            };
            return View(hotelEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HotelEditVM model)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var data = await _hotelService.GetHotelDetail(id);
            if (data == null)
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            await _hotelService.Update(id, model);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            await _hotelImageService.Delete(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SetMain(int imageId, int hotelId)
        {
            if (imageId == null)
            {
                return BadRequest();
            }
            await _hotelImageService.SetMain(imageId, hotelId);
            return Ok();
        }

        [HttpGet]
        public IActionResult AddImages(int id)
        {
            AddImageVM vm = new AddImageVM()
            {
                HotelId = id,
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImages(AddImageVM model)
        {
            if(model == null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            await _hotelImageService.AddImage(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Change(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var existImage = await _hotelImageService.GetById(id);
            if(existImage == null)
            {
                return NotFound();
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Change(int id, ChangeImageVM model)
        {
            var existData = await _hotelImageService.GetById(id);
            if(existData == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _hotelImageService.Change(id, model);
            return RedirectToAction("Index");
        }
    }
}
