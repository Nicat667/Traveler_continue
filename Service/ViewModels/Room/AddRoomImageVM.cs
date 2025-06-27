using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Room
{
    public class AddRoomImageVM
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
