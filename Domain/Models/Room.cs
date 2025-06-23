using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Room : BaseEntity
    {
        public RoomType Type { get; set; }
        public string Description { get; set; }
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public int GuestCapacity { get; set; }
        public int BedCount { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<RoomImage> RoomImages = new List<RoomImage>();
        public ICollection<Reservation> Reservations = new List<Reservation>();
    }
}
