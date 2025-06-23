using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class WishList : BaseEntity
    {
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
