using Domain.Models;
using Microsoft.AspNetCore.Http;
using Service.ViewModels.HotelImages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IHotelImageService
    {
        Task Create(List<HotelImageCreateVM> model);
        Task Delete(int id);
        Task SetMain(int id, int hotelId);
        Task Change(int id, ChangeImageVM model);
        Task AddImage(AddImageVM model);
        Task<HotelImageVM> GetById(int id);
        Task<IEnumerable<HotelImageVM>> GetAllByHotelId(int id);   
    }
}
