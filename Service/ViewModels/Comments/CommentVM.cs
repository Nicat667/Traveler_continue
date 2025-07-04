using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Comments
{
    public class CommentVM
    {
        [Required]
        public int HotelId { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public int Rate { get; set; }
    }
}
