using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.HotelImages
{
    public class AddImageVM
    {
        public int HotelId { get; set; }
        public IEnumerable<IFormFile> NewImage { get; set; }
    }
}
