using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Room
{
    public class BookVM
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public int RoomCount { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
