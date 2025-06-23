using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.City
{
    public class CityEditVM
    {
        [Required]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string ExistImage { get; set; }
    }
}
