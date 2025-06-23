using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.City
{
    public class CityVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
        public int HotelCount { get; set; }
    }
}
