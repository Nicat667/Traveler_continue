using Domain.Models;
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
        private readonly IBlobStorage _blobStorage;
        public StaffService(IStaffRepository staffRepository, IBlobStorage blobStorage)
        {
            _staffRepository = staffRepository;
            _blobStorage = blobStorage;
        }

        public async Task Create(StaffCreateVM model)
        {
            await _staffRepository.CreateAsync(new Staff { Name = model.Name, Position = model.Position, Image = await _blobStorage.UploadAsync(model.Image) });

        }

        public async Task Delete(int id)
        {
            var data = await _staffRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _staffRepository.DeleteAsync(data);
                await _blobStorage.DeleteAsync(data.Image);
            }
        }

        public async Task<IEnumerable<StaffVM>> GetAll()
        {
            var datas = await _staffRepository.GetAllAsync();
            List<StaffVM> result = new List<StaffVM>();
            foreach (var data in datas)
            {
                StaffVM vm = new StaffVM()
                {
                    Name = data.Name,
                    Image = data.Image,
                    Id = data.Id,
                    ImageUrl = await _blobStorage.GetBlobUrlAsync(data.Image),
                    Position = data.Position,
                };
                result.Add(vm);
            }
            return result;
        }

        public async Task<StaffVM> GetById(int id)
        {
            var data = await _staffRepository.GetByIdAsync(id);
            StaffVM staffVM = new StaffVM()
            {
                Name= data.Name,
                ImageUrl = await _blobStorage.GetBlobUrlAsync(data.Image),
                Position = data.Position,
            };
            return staffVM;
        }

        public async Task Update(int id, StaffEditVM model)
        {
            var existData = await _staffRepository.GetByIdAsync(id);

            existData.Name = model.Name;
            existData.Position = model.Position;
            if (model.NewImage != null)
            {
                existData.Image = await _blobStorage.ReplaceAsync(existData.Image, model.NewImage);
            }
            await _staffRepository.UpdateAsync(existData);
        }
    }
}
