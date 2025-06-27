using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Room
{
    public class RoomVM
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public int GuestCapacity { get; set; }
        public int BedCount { get; set; }
        public int HotelId { get; set; }
        public string ImageUrl { get; set; }
    }
}
