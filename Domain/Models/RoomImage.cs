using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RoomImage : BaseEntity
    {
        public bool IsMain { get; set; }
        public string Name { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
