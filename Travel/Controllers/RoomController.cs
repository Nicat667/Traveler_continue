using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModels.Room;
using System.Threading.Tasks;

namespace Travel.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public RoomController(IRoomService roomService, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _roomService = roomService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            return View(await _roomService.GetRoomById(id));
        }

        public async Task<IActionResult> Book(BookVM model)
        {
            if(model==null)
            {
                return BadRequest();
            }
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                TempData["BookingError"] = "Please log in to book a room.";
                return RedirectToAction("Detail", new { id = model.RoomId });
            }
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            AppUser user = await _userManager.FindByNameAsync(userName);
            model.UserId = user.Id;
            if (!await _roomService.BookRoom(model))
            {
                TempData["BookingError"] = "Not enough rooms available or time range is already booked..";
                return RedirectToAction("Detail", new { id = model.RoomId });
            }

            
            TempData["BookingSuccess"] = true;
            return RedirectToAction("Detail", new { id = model.RoomId });
        }
    }
}
