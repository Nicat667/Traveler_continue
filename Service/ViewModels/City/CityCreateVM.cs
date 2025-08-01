﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.City
{
    public class CityCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
