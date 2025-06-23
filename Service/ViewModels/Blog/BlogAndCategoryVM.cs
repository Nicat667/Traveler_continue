using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Blog
{
    public class BlogAndCategoryVM
    {
        public IEnumerable<BlogDetailVM> Blogs { get; set; }
        public IEnumerable<BlogCategoryVM> BlogCategories { get; set; }
        public BlogDetailVM Blog { get; set; }
    }
}
