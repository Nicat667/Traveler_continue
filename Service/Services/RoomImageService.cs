using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using Service.ViewModels.Room;
using Service.ViewModels.RoomImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class RoomImageService : IRoomImageService
    {
        private readonly IRoomImageRepository _roomImageRepository;
        private readonly IBlobStorage _blobStorage;
        public RoomImageService(IRoomImageRepository roomImageRepository, IBlobStorage blobStorage)
        {
            _roomImageRepository = roomImageRepository;
            _blobStorage = blobStorage;
        }

        public async Task AddImages(AddRoomImageVM model)
        { 
            foreach(var image in model.Images)
            {
                RoomImage roomImage = new RoomImage()
                {
                    RoomId = model.RoomId,
                    IsMain = false,
                    Name = await _blobStorage.UploadAsync(image),
                };
                await _roomImageRepository.CreateAsync(roomImage);
            }
        }

        public async Task Change(int id, ChangeRoomImageVM model)
        {
            var image = await _roomImageRepository.GetByIdAsync(id);
            image.Name = await _blobStorage.ReplaceAsync(image.Name, model.Image);
            await _roomImageRepository.UpdateAsync(image);
        }

        public async Task Create(RoomImageCreateVM model)
        {
            RoomImage roomImage = new RoomImage()
            {
                IsMain = model.IsMain,
                Name = await _blobStorage.UploadAsync(model.NewImage),
                RoomId = model.RoomId,
            };
            await _roomImageRepository.CreateAsync(roomImage);
        }

        public async Task Delete(int id)
        {
            var data = await _roomImageRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _roomImageRepository.DeleteAsync(data);
                await _blobStorage.DeleteAsync(data.Name);
            }
        }

        public async Task<IEnumerable<RoomImageVM>> GetAllByRoomId(int id)
        {
            var datas = await _roomImageRepository.GetAllByRoomId(id);
            List<RoomImageVM> result = new List<RoomImageVM>();
            foreach (var data in datas)
            {
                RoomImageVM vm = new RoomImageVM()
                {
                    Id = data.Id,
                    IsMain = data.IsMain,
                    Url = await _blobStorage.GetBlobUrlAsync(data.Name),
                    RoomId=data.RoomId,
                };
                result.Add(vm);
            }
            return result;
        }

        public async Task<RoomImageVM> GetById(int id)
        {
            var data = await _roomImageRepository.GetByIdAsync(id);
            return new RoomImageVM
            {
                Id = id,
                IsMain = data.IsMain,
                RoomId = data.RoomId,
                Url = await _blobStorage.GetBlobUrlAsync(data.Name)
            };
        }

        public async Task SetMain(int id, int roomId)
        {
            var roomImages = await _roomImageRepository.GetRoomImagesByRoomId(roomId);
            foreach (var item in roomImages)
            {
                if (item.IsMain)
                {
                    item.IsMain = false;
                }
            }
            roomImages.FirstOrDefault(m=>m.Id == id).IsMain = true;
            List<RoomImage> newRoomImages = new List<RoomImage>();
            foreach (var item in roomImages)
            {
                RoomImage image = new RoomImage()
                {
                    IsMain = item.IsMain,
                    RoomId = item.RoomId,
                    Id = item.Id,
                    Name = item.Name,
                };
                newRoomImages.Add(image);
            }
            await _roomImageRepository.UpdateRange(newRoomImages);

            
        }
    }
}
