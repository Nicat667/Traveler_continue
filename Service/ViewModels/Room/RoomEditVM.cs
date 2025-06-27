using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Service.ViewModels.RoomImages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Room
{
    public class RoomEditVM
    {
        public int Id { get; set; }
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
        public IEnumerable<RoomImageVM> Images { get; set; }
        [Required]
        public string Describtion { get; set; }
        public int HotelId { get; set; }
    }
}
