using Service.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICommentService
    {
        Task AddComment(CommentVM comment);
        Task<IEnumerable<CommentVM>> ShowMore(int hotelId, int skip);
        Task<IEnumerable<CommentAdminVM>> GetAll();
    }
}
