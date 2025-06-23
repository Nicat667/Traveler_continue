using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Comment : BaseEntity
    {
        public string AuthorName { get; set; }
        public string Message { get; set; }
        public int Rate { get; set; }
        public DateTime Created { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

    }
}
