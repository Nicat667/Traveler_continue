using Service.ViewModels.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffVM>> GetAll();
        Task<StaffVM> GetById(int id);
        Task Create(StaffCreateVM model);
        Task Delete(int id);
        Task Update(int id, StaffEditVM model);
    }
}
