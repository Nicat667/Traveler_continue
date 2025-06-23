using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using Service.ViewModels.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }
        public async Task<IEnumerable<StaffVM>> GetAll()
        {
            var datas = await _staffRepository.GetAllAsync();
            return datas.Select(m => new StaffVM
            {
                Name = m.Name,
                Image = m.Image,
                Position = m.Position,
            });
        }
    }
}
