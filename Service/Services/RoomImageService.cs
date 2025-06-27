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
    }
}
