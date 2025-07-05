using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;
using Service.Services.Interfaces;
using Service.ViewModels.Room;
using Service.ViewModels.RoomImages;
using System.Threading.Tasks;

namespace Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IHotelService _hotelService;
        private readonly IRoomImageService _roomImageService;
        public RoomController(IRoomService roomService, IHotelService hotelService, IRoomImageService roomImageService)
        {
            _roomService = roomService;
            _hotelService = hotelService;
            _roomImageService = roomImageService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            return View(await _roomService.GetRoomsByHotelIdWithoutGrouping(id));
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync(int id)
        {
            ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>().Select(rt => new SelectListItem
            {
                Text = rt.ToString(),
                Value = ((int)rt).ToString()
            }).ToList();
            ViewBag.HotelId = id;
      
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomCreateVM model)
        {
            ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>().Select(rt => new SelectListItem
            {
                Text = rt.ToString(),
                Value = ((int)rt).ToString()
            }).ToList();
            ViewBag.HotelId = model.HotelId;
            if (model.Images == null || model.Images.Count() < 5) 
            {
                ModelState.AddModelError("Images", "Please upload at least 5 images.");
                return View(model);
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _roomService.Create(model);
            return RedirectToAction("Detail", "Hotel", new { id = model.HotelId });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var data = await _roomService.GetRoomById(id);
            if (data == null)
            {
                return NotFound();
            }
            await _roomService.Delete(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var data = await _roomService.GetRoomById(id);
            if(data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>().Select(rt => new SelectListItem
            {
                Text = rt.ToString(),
                Value = ((int)rt).ToString()
            }).ToList();
            if (id == null)
            {
                return BadRequest();
            }
            var data = await _roomService.GetRoomById(id);
            if (data == null)
            {
                return NotFound();
            }
            RoomEditVM vm = new RoomEditVM()
            {
                Type = Enum.Parse<RoomType>(data.Type),
                Area = data.Area,
                Price = data.Price,
                BedCount = data.BedCount,
                GuestCapacity = data.GuestCapacity,
                Describtion = data.Description,
                HotelId = data.HotelId,
                Id = data.Id,
                Images = data.ImagesUrls,

            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomEditVM model)
        {
            ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>().Select(rt => new SelectListItem
            {
                Text = rt.ToString(),
                Value = ((int)rt).ToString()
            }).ToList();
            if (id == null)
            {
                return BadRequest();
            }
            var data = await _roomService.GetRoomById(id);
            if (data == null)
            {
                return NotFound();
            }
            await _roomService.Edit(id, model);
            return RedirectToAction("Index", "Room", new { id = data.HotelId });

        }

        [HttpGet]
        public async Task<IActionResult> AddImages(int id)
        {
            var data = await _roomService.GetRoomById(id);
            AddRoomImageVM addRoomImageVM = new AddRoomImageVM()
            {
                RoomId = id,
                HotelId = data.HotelId,
            };
            return View(addRoomImageVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImages(AddRoomImageVM model)
        {

            if (model == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _roomImageService.AddImages(model);
            return RedirectToAction("Index", "Room", new { id = model.HotelId });
        }

        [HttpGet]
        public async Task<IActionResult> Change(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var existImage = await _roomImageService.GetById(id);
            if (existImage == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Change(int id, ChangeRoomImageVM model)
        {
            var existData = await _roomImageService.GetById(id);
            var hotel = await _roomService.GetRoomById(existData.RoomId);
            if (existData == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _roomImageService.Change(id, model);
            return RedirectToAction("Index", "Room", new { id = hotel.HotelId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var data = await _roomImageService.GetById(id);
            if(data == null)
            {
                return NotFound();
            }
            await _roomImageService.Delete(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SetMain(int imageId, int roomId)
        {
            if(imageId == null)
            {
                return BadRequest();
            }
            await _roomImageService.SetMain(imageId, roomId);
            return Ok();
        }
    }
}
