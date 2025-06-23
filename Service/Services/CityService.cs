using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using Service.ViewModels.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IBlobStorage _blobStorage;
        public CityService(ICityRepository cityRepository, IBlobStorage blobStorage)
        {
            _cityRepository = cityRepository;
            _blobStorage = blobStorage;
        }

        public async Task Create(CityCreateVM model)
        {
            await _cityRepository.CreateAsync(new City
            {
                Name = model.Name,
                Image = await _blobStorage.UploadAsync(model.Image),
            });
        }

        public async Task Delete(int id)
        {
            var data = await _cityRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _cityRepository.DeleteAsync(data);
                await _blobStorage.DeleteAsync(data.Name);
            }
        }

        public async Task Edit(int id, CityEditVM model)
        {
            var existData = await _cityRepository.GetByIdAsync(id);

            existData.Name = model.Name;

            if (model.Image != null)
            {
                existData.Image = await _blobStorage.ReplaceAsync(existData.Image, model.Image);
            }

            await _cityRepository.UpdateAsync(existData);
        }

        public async Task<IEnumerable<CityVM>> GetAllWihHotelsAsync()
        {
            var cities = await _cityRepository.GetAllWithHotels();
            List<CityVM> result = new List<CityVM>();

            foreach (var city in cities)
            {
                var imageUrl = await _blobStorage.GetBlobUrlAsync(city.Image); 

                result.Add(new CityVM
                {
                    Id = city.Id,
                    Name = city.Name,
                    Image = city.Image,
                    ImageUrl = imageUrl,
                    HotelCount = city.Hotels.Count,
                });
            }

            return result;
        }

        public async Task<CityVM> GetById(int id)
        {
            var data = await _cityRepository.GetByIdWithHotel(id);
            return new CityVM
            {
                Id = data.Id,
                Name = data.Name,
                Image = data.Image,
                ImageUrl = await _blobStorage.GetBlobUrlAsync(data.Image),
                HotelCount= data.Hotels.Count
            };
        }
    }
}
