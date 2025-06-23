using Service.ViewModels.Blog;
using Service.ViewModels.City;
using Service.ViewModels.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<CityVM> Cities { get; set; }
        public IEnumerable<BlogVM> Blogs { get; set; }
    }
}
