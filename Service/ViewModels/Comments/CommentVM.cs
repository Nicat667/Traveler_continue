using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Comments
{
    public class CommentVM
    {
        public int HotelId { get; set; }
        public string Message { get; set; }
        public string AuthorName { get; set; }
        public int Rate { get; set; }
    }
}
