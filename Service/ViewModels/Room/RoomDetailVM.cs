using Domain.Models;
using Service.ViewModels.RoomImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Room
{
    public class RoomDetailVM
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public int GuestCapacity { get; set; }
        public int BedCount { get; set; }
        public int HotelId { get; set; }
        public string Description { get; set; }
        public IEnumerable<RoomImageVM> ImagesUrls { get; set; }
        public IEnumerable<RoomVM> Options { get; set; }
    }
}
