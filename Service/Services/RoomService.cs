using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories.Interfaces;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using Service.ViewModels.Room;
using Service.ViewModels.RoomImages;

namespace Service.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationService _reservationService;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBlobStorage _blobStorage;
        private readonly IRoomImageService _roomImageService;
        public RoomService(IRoomRepository roomRepository, 
                           IReservationService reservationService, 
                           IEmailService emailService, 
                           UserManager<AppUser> userManager,
                           IBlobStorage blobStorage,
                           IRoomImageService roomImageService)
        {
            _roomRepository = roomRepository;
            _reservationService = reservationService;
            _emailService = emailService;
            _userManager = userManager;
            _blobStorage = blobStorage;
            _roomImageService = roomImageService;
        }

        public async Task<bool> BookRoom(BookVM model)
        {
            var rooms = await _roomRepository.GetAllRoomsWithReservationAndHotel();

            var requestedRoom = rooms.FirstOrDefault(m => m.Id == model.RoomId);
            if (requestedRoom == null) return false;

            var sameGroupRooms = rooms
                .Where(m =>
                    m.HotelId == model.HotelId &&
                    m.Type == requestedRoom.Type &&
                    m.BedCount == requestedRoom.BedCount &&
                    m.GuestCapacity == requestedRoom.GuestCapacity &&
                    m.Area == requestedRoom.Area &&
                    m.Price == requestedRoom.Price)
                .ToList();

            List<Room> availableRooms = sameGroupRooms
                .Where(room => !room.Reservations.Any(r =>
                    r.StartDate < model.EndDate && model.StartDate < r.EndDate))
                .ToList();

            if (availableRooms.Count >= model.RoomCount)
            {
                foreach (var room in availableRooms.Take(model.RoomCount))
                {
                    await _reservationService.AddRoomToReservation(new Reservation
                    {
                        RoomId = room.Id,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        AppUserId = model.UserId,
                    });
                }



                var user = await _userManager.FindByIdAsync(model.UserId);
                string html = string.Empty;
                using (StreamReader sr = new StreamReader("wwwroot/templates/EmailpageForReservation.html"))
                {
                    html = sr.ReadToEnd();
                }
                html = html.Replace("{fullname}", user.FullName);
                html = html.Replace("{type}", requestedRoom.Type.ToString());
                html = html.Replace("{hotel}", requestedRoom.Hotel.Name);
                html = html.Replace("{count}", model.RoomCount.ToString());
                html = html.Replace("{time}", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                _emailService.Send(user.Email, "Notification", html);

                return true;
            }

            return false;
        }

        public async Task Create(RoomCreateVM model)
        {
            Room room = new Room()
            {
                Type = model.Type,
                Area = model.Area,
                Price = model.Price,
                GuestCapacity = model.GuestCapacity,
                BedCount = model.BedCount,
                Description = model.Describtion,
                HotelId = model.HotelId,
            };
            await _roomRepository.CreateAsync(room);
            List<RoomImageCreateVM> images = new List<RoomImageCreateVM>();
            foreach(var item in model.Images)
            {
                RoomImageCreateVM image = new RoomImageCreateVM()
                {
                    IsMain = false,
                    RoomId = room.Id,
                    NewImage = item,
                };
                images.Add(image);
                
            }
            images.FirstOrDefault().IsMain = true;
            foreach (var image in images)
            {
                await _roomImageService.Create(image);
            }
        }

        public async Task Delete(int id)
        {
            var data = await _roomRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _roomRepository.DeleteAsync(data);
                foreach (var item in data.RoomImages)
                {
                    await _blobStorage.DeleteAsync(item.Name);
                }
            }
        }

        public async Task Edit(int id, RoomEditVM model)
        {
            var existData = await _roomRepository.GetRoomById(id);
            existData.Description = model.Describtion;
            existData.Price = model.Price;
            existData.Area = model.Area;
            existData.BedCount = model.BedCount;
            existData.GuestCapacity = model.GuestCapacity;
            existData.Type = model.Type;
            existData.HotelId = model.HotelId;
            await _roomRepository.UpdateAsync(existData);
        }

        public async Task<IEnumerable<RoomVM>> GetAllRooms()
        {
            var datas = await _roomRepository.GetAllRooms();
            List<RoomVM> rooms = new List<RoomVM>();
            foreach (var data in datas)
            {
                RoomVM room = new RoomVM()
                {
                    Area = data.Area,
                    Price = data.Price,
                    BedCount = data.BedCount,
                    GuestCapacity = data.GuestCapacity,
                    HotelId = data.HotelId,
                    Type = data.Type.ToString(),
                    Id = data.Id,
                    ImageUrl = await _blobStorage.GetBlobUrlAsync(data.RoomImages.FirstOrDefault(m => m.IsMain == true).Name)
                };
                rooms.Add(room);
            }
            return rooms;
        }

        public async Task<RoomDetailVM> GetRoomById(int id)
        {
            var data = await _roomRepository.GetRoomById(id);
            var options = await GetRoomsByHotelId(data.HotelId);
            RoomDetailVM roomDetailVM = new RoomDetailVM()
            {
                Id = data.Id,
                Area = data.Area,
                Price = data.Price,
                BedCount = data.BedCount,
                GuestCapacity = data.GuestCapacity,
                HotelId = data.HotelId,
                Type = data.Type.ToString(),
                Description = data.Description,
                Options = options.Where(r => r.HotelId == data.HotelId && r.Price != data.Price && r.Id != data.Id).ToList(),
            };
            List<RoomImageVM> imageWithUrls = new List<RoomImageVM>();
            foreach(var image in data.RoomImages)
            {
                RoomImageVM imageVM = new RoomImageVM()
                {
                    Id= image.Id,
                    IsMain = image.IsMain,
                    RoomId = image.RoomId,
                    Url = await _blobStorage.GetBlobUrlAsync(image.Name)
                };
                imageWithUrls.Add(imageVM);
            }

            roomDetailVM.ImagesUrls = imageWithUrls;
            return roomDetailVM;
        }

        public async Task<List<RoomVM>> GetRoomsByHotelId(int hotelId)
        {
            var datas = await _roomRepository.GetRoomsByHotelId(hotelId);

            var roomVMs = new List<RoomVM>();

            foreach (var group in datas.GroupBy(r => new { r.GuestCapacity, r.BedCount, r.Area, r.Price }))
            {
                var m = group.First();

                var roomVM = new RoomVM
                {
                    Id = m.Id,
                    Area = m.Area,
                    Price = m.Price,
                    BedCount = m.BedCount,
                    GuestCapacity = m.GuestCapacity,
                    HotelId = m.HotelId,
                    Type = m.Type.ToString(),
                    ImageUrl = await _blobStorage.GetBlobUrlAsync(m.RoomImages.FirstOrDefault(m => m.IsMain).Name),
                };

                roomVMs.Add(roomVM);
            }

            return roomVMs;
        }

        public async Task<IEnumerable<RoomVM>> GetRoomsByHotelIdWithoutGrouping(int hotelId)
        {
            var datas = await _roomRepository.GetRoomsByHotelId(hotelId);

            var roomVMs = new List<RoomVM>();

            foreach (var m in datas)
            {
                var roomVM = new RoomVM
                {
                    Id = m.Id,
                    Area = m.Area,
                    Price = m.Price,
                    BedCount = m.BedCount,
                    GuestCapacity = m.GuestCapacity,
                    HotelId = m.HotelId,
                    Type = m.Type.ToString(),
                    ImageUrl = await _blobStorage.GetBlobUrlAsync(m.RoomImages.FirstOrDefault(m => m.IsMain).Name),
                };

                roomVMs.Add(roomVM);
            }

            return roomVMs;
        }


    }
}
