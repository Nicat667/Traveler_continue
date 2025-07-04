using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModels.Comments;

namespace Travel.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddComment(CommentVM comment)
        {
            if(comment == null)
            {
                return BadRequest();
            }

            await _commentService.AddComment(comment);
            return RedirectToAction("Index", "Hotel");
        }
    }
}
