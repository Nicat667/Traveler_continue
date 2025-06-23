using Microsoft.AspNetCore.Mvc;

namespace Reservation_Final.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
