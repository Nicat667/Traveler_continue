using Domain.Models;
using Service.ViewModels.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISettingService
    {
        Task<IEnumerable<SettingVM>> GetSettings();
        Task<SettingVM> GetById(int id);
        Task Edit(int id, SettingEditVM model);
    }
}
