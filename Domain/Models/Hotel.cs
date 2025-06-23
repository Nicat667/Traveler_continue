using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Hotel : BaseEntity
    {
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
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<Comment> Comments = new List<Comment>();
        public ICollection<Room> Rooms = new List<Room>();
        public ICollection<HotelImage> HotelImages = new List<HotelImage>();
        public ICollection<WishList> WishLists = new List<WishList>();

    }
}
