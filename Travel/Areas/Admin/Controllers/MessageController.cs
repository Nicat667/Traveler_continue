using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModels.Messages;
using System.Threading.Tasks;

namespace Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        public readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _messageService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var data = await _messageService.GetDetail(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var data = await _messageService.GetDetail(id);
            if (data == null)
            {
                return NotFound();
            }
            await _messageService.Delete(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var data = await _messageService.GetDetail(id);
            if (data == null)
            {
                return NotFound();
            }
            await _messageService.Update(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Answer(int id)
        {
            await _messageService.Update(id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Answer(int id, AnswerVM model)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var data = await _messageService.GetDetail(id);
            if(data == null)
            {
                return NotFound();
            }
            await _messageService.Answer(id, model);
            return RedirectToAction("Index");
        }
    }
}
