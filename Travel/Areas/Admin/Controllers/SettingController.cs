using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModels.Setting;
using System.Threading.Tasks;

namespace Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _settingService.GetSettings());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var data = await _settingService.GetById(id);
            if(data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var existData = await _settingService.GetById(id);
            if(existData == null)
            {
                return NotFound();
            }
            return View(new SettingEditVM { Key = existData.Key, Value = existData.Value, ExistFile = existData.Value});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SettingEditVM model)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var existData = await _settingService.GetById(id);
            if (existData == null)
            {
                return NotFound();
            }
            await _settingService.Edit(id, model);  
            return RedirectToAction("Index");
        }
    }
}
