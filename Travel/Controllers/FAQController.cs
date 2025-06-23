using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using System.Threading.Tasks;

namespace Reservation_Final.Controllers
{
    public class FAQController : Controller
    {
        private readonly IFAQService _faqService;
        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _faqService.GetAll());
        }

    }
}
