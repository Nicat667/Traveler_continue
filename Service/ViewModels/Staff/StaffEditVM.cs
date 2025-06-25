using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Staff
{
    public class StaffEditVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        public string ExistImage { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
