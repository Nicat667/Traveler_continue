using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using Service.ViewModels.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IBlobStorage _blobStorage;
        public SettingService(ISettingRepository settingRepository, IBlobStorage blobStorage)
        {
            _settingRepository = settingRepository;
            _blobStorage = blobStorage;
        }

        public async Task Edit(int id, SettingEditVM model)
        {
            var existData = await _settingRepository.GetByIdAsync(id);

            if(existData.Key == "logo")
            {
                existData.Value = await _blobStorage.ReplaceAsync(existData.Value, model.File);
            }
            else if(existData.Key == "video")
            {
                existData.Value = await _blobStorage.ReplaceAsync(existData.Value, model.File);
            }
            else
            {
                existData.Value = model.Value;
            }
            await _settingRepository.UpdateAsync(existData);
        }

        public async Task<SettingVM> GetById(int id)
        {
            var data = await _settingRepository.GetByIdAsync(id);
            if(data.Key == "logo")
            {
                data.Value = await _blobStorage.GetBlobUrlAsync(data.Value);
            }
            if(data.Key == "video")
            {
                data.Value = await _blobStorage.GetBlobUrlAsync(data.Value);
            }
            return new SettingVM { Key =  data.Key, Value = data.Value, Id = data.Id };
        }

        public async Task<IEnumerable<SettingVM>> GetSettings()
        {
            var datas = await _settingRepository.GetAllSettingAsync();
            foreach(var setting in datas)
            {
                if(setting.Key == "logo")
                {
                    setting.Value = await _blobStorage.GetBlobUrlAsync(setting.Value);
                }
                else if (setting.Key == "video")
                {
                    setting.Value = await _blobStorage.GetBlobUrlAsync(setting.Value);
                }
            }
            return datas.Select(x => new SettingVM()
            {
                Key = x.Key,
                Value = x.Value,
                Id = x.Id
            });
        }
    }
}
