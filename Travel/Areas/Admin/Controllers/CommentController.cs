using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using System.Threading.Tasks;

namespace Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _commentService.GetAll());
        }
    }
}
