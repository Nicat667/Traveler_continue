using Domain.Models;
using Service.ViewModels.HotelImages;
using Service.ViewModels.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Hotel
{
    public class HotelDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int StarCount { get; set; }
        public bool AirConditioning { get; set; }
        public bool AirportTransport { get; set; }
        public bool FitnessCenter { get; set; }
        public bool Parking { get; set; }
        public bool Restaurant { get; set; }
        public bool SpaSauna { get; set; }
        public decimal Rate { get; set; }
        public IEnumerable<HotelImage> Images { get; set; }
        public IEnumerable<HotelImageVM> ImageUrls { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<RoomVM> Rooms { get; set; }
    }
}
