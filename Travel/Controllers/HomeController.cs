using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModels;

namespace Travel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IBlogService _blogService;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ICityService cityService, IBlogService blogService, ILogger<HomeController> logger)
        {
            _cityService = cityService;
            _blogService = blogService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Cities = await _cityService.GetAllWihHotelsAsync(),
                Blogs = await _blogService.GetAllWithCategories(),
            };
            _logger.LogInformation("Visited Home Page at {Time}", DateTime.Now);
            return View(homeVM);
        }

        
    }
}
