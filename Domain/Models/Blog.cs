using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Blog : BaseEntity
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuthorImage { get; set; }
        public string AuthorName { get; set; }
        public int CategoryId { get; set; }
        public bool IsVisible { get; set; }
        public BlogCategory BlogCategory { get; set; }
    }
}
