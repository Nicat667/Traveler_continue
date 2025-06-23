using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Blog
{
    public class BlogDetailVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CategoryName { get; set; }
        public string AuthorImage { get; set; }
        public string AuthorName { get; set; }
        public string CreateDate { get; set; }
    }
}
