using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Service.ViewModels.RoomImages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Room
{
    public class RoomCreateVM
    {
        [Required]
        public RoomType Type { get; set; }
        [Required]
        public decimal Area { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int GuestCapacity { get; set; }
        [Required]
        public int BedCount { get; set; }
        [Required]
        public int HotelId { get; set; }
        [Required]
        public IEnumerable<IFormFile> Images { get; set; }
        [Required]
        public string Describtion { get; set; }
    }
}
