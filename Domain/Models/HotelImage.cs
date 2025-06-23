using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class HotelImage : BaseEntity
    {
        public bool IsMain { get; set; }
        public string Name { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
