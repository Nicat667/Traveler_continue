using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModels;
using Service.ViewModels.Hotel;
using System.Threading.Tasks;

namespace Reservation_Final.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly ICommentService _commentService;
        public HotelController(IHotelService hotelService, ICommentService commentService)
        {
            _hotelService = hotelService;
            _commentService = commentService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _hotelService.GetAllHotel());
        }
        public async Task<IActionResult> Detail(int id)
        {
            return View(await _hotelService.GetHotelDetail(id));
        }
        public async Task<IActionResult> Filter(FilterVM filter)
        {
            return View(await _hotelService.HotelFilter(filter));
        }
        public async Task<IActionResult> FilterByCity(int id)
        {
            return View(await _hotelService.HotelFilterByCity(id));
        }
        public async Task<IActionResult> Search(SearchVM query)
        {
            return View(await _hotelService.Search(query));
        }
        public async Task<IActionResult> ShowMore(int hotelId, int skip)
        {
            return Ok(await _commentService.ShowMore(hotelId, skip));
        }
    }
}
