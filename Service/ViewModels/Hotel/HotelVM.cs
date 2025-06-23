using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Hotel
{
    public class HotelVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int StarCount { get; set; }
        public string MainImage { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal Rate { get; set; }
        public int CommentCount { get; set; }
        public int CityId { get; set; }
    }
}
