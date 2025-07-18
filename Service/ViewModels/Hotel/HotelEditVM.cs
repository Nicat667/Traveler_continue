﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.HotelImages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Hotel
{
    public class HotelEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int StarCount { get; set; }
        [Required]
        public bool AirConditioning { get; set; }
        [Required]
        public bool AirportTransport { get; set; }
        [Required]
        public bool FitnessCenter { get; set; }
        [Required]
        public bool Parking { get; set; }
        [Required]
        public bool Restaurant { get; set; }
        [Required]
        public bool SpaSauna { get; set; }
        [BindNever]
        public IEnumerable<HotelImageVM> ExistImages { get; set; }
    }
}
