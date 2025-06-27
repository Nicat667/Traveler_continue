using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.RoomImages
{
    public class RoomImageCreateVM
    {
        public bool IsMain { get; set; }
        public IFormFile NewImage { get; set; }
        public int RoomId { get; set; }
    }
}
