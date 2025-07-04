using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModels.Messages;

namespace Reservation_Final.Controllers
{
    public class ContactController : Controller
    {
        private readonly IMessageService _messageService;
        public ContactController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMessage(MessageCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index",model);
            }
            await _messageService.Create(model);
            return RedirectToAction("Index");
        }
    }
}
