using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Reservation_Final.Controllers
{
    public class AboutController : Controller
    {
        private readonly IStaffService _staffService;
        public AboutController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _staffService.GetAll());
        }
        
    }
}
