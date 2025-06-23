using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Staff : BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

    }
}
