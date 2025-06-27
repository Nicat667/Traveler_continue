using Domain.Models;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using Service.ViewModels.HotelImages;

namespace Service.Services
{
    public class HotelImageService : IHotelImageService
    {
        private readonly IHotelImageRepository _repository;
        private readonly IBlobStorage _BlobStorage;
        public HotelImageService(IHotelImageRepository repository, IBlobStorage blobStorage)
        {
            _repository = repository;
            _BlobStorage = blobStorage;
        }

        public async Task AddImage(AddImageVM model)
        {
            foreach(var item in model.NewImage)
            {
                HotelImage hotelImage = new HotelImage()
                {
                    IsMain = false,
                    HotelId = model.HotelId,
                    Name = await _BlobStorage.UploadAsync(item)
                };
                await _repository.CreateAsync(hotelImage);
            }
            
            
        }

        public async Task Change(int id, ChangeImageVM model)
        {
            var image = await _repository.GetByIdAsync(id);
            image.Name = await _BlobStorage.ReplaceAsync(image.Name, model.NewImage);
            await _repository.UpdateAsync(image);
        }

        public async Task Create(List<HotelImageCreateVM> model)
        {
            List<HotelImage> images = new List<HotelImage>();
            foreach(var item in model)
            {
                HotelImage hotelImage = new HotelImage()
                {
                    IsMain = item.IsMain,
                    HotelId = item.HotelId,
                    Name = item.Name,
                };
                images.Add(hotelImage);
            }
            
            await _repository.CreateRange(images);
        }

        public async Task Delete(int id)
        {
            var image = await _repository.GetByIdAsync(id);
            if(image != null)
            {
                await _repository.DeleteAsync(image);
                await _BlobStorage.DeleteAsync(image.Name);
            }
        }

        public async Task<HotelImageVM> GetById(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            HotelImageVM vm = new HotelImageVM()
            {
                IsMain = data.IsMain,
                HotelId = data.HotelId,
                Id = data.Id,
                Url = await _BlobStorage.GetBlobUrlAsync(data.Name),
            };
            return vm;
        }

        public async Task SetMain(int imageId, int hotelId)
        {
            var allImages = await _repository.GetImagesByHotelId(hotelId);
            var newMain = await _repository.GetByIdAsync(imageId);
            foreach(var image in allImages)
            {
                if (image.IsMain)
                {
                    image.IsMain = false;
                }
            }
            allImages.FirstOrDefault(m => m.Id == imageId).IsMain = true;
            await _repository.UpdateRange(allImages);
        }
    }
}
