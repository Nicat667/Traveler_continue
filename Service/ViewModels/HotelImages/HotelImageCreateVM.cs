using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.HotelImages
{
    public class HotelImageCreateVM
    {
        public bool IsMain { get; set; }
        public string Name { get; set; }
        public int HotelId { get; set; }
    }
}
