using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using Service.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task AddComment(CommentVM comment)
        {
            await _commentRepository.CreateAsync(new Domain.Models.Comment
            {
                AuthorName = comment.AuthorName,
                Rate = comment.Rate,
                Message = comment.Message,
                HotelId = comment.HotelId,
                Created = DateTime.Now,
            });
        }

        public async Task<IEnumerable<CommentAdminVM>> GetAll()
        {
            var datas = await _commentRepository.GetAllWithHotels();
            return datas.Select(x => new CommentAdminVM
            {
                AuthorName = x.AuthorName,
                Rate = x.Rate,
                Message = x.Message,
                Created = x.Created,
                Hotel = x.Hotel.Name,
            });
        }

        public async Task<IEnumerable<CommentVM>> ShowMore(int hotelId, int skip)
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentsByHotelId  = comments.Where(m=>m.HotelId == hotelId);
            return commentsByHotelId.Skip(skip).Take(3).Select(m=> new CommentVM
            {
                HotelId=m.HotelId,
                AuthorName = m.AuthorName,
                Message = m.Message,
                Rate = m.Rate,
            });
        }
    }
}
