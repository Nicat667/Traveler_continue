using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.HotelImages
{
    public class ChangeImageVM
    {
        public IFormFile NewImage { get; set; }
    }
}
