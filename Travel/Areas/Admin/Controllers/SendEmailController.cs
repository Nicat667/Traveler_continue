using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModels;
using System.Security.Policy;

namespace Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SendEmailController : Controller
    {
        private readonly IEmailTableService _emailTableService;
        private readonly IEmailService _emailService;
        private readonly IEmailTableService _emails;
        public SendEmailController(IEmailTableService emailTableService, IEmailService emailService, IEmailTableService emails)
        {
            _emailTableService = emailTableService;
            _emailService = emailService;
            _emails = emails;
        }
        
        public IActionResult Index()
        {
            return View();
        }

    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(SendEmailVM model)
        {
            var emails = await _emails.GetAll();
            string html = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/templates/MessageForAll.html"))
            {
                html = reader.ReadToEnd();
            }
            html = html.Replace("{message}", model.Context);
            foreach(var email in emails)
            {
                _emailService.Send(email.EmailAddress, "Announcement!", html);
            }
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
