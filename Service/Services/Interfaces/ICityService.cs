using Domain.Models;
using Service.ViewModels.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityVM>> GetAllWihHotelsAsync();
        Task<CityVM> GetById(int id);
        Task Create(CityCreateVM model);
        Task Delete(int id);
        Task Edit(int id, CityEditVM model);
        Task<IEnumerable<CityVM>> GetAll();
    }
}
