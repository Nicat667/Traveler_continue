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
        public RoomService(IRoomRepository roomRepository, 
                           IReservationService reservationService, 
                           IEmailService emailService, 
                           UserManager<AppUser> userManager,
                           IBlobStorage blobStorage)
        {
            _roomRepository = roomRepository;
            _reservationService = reservationService;
            _emailService = emailService;
            _userManager = userManager;
            _blobStorage = blobStorage;
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
                    m.GuestCapacity == requestedRoom.GuestCapacity)
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
                    RoomId = image.Id,
                    Url = await _blobStorage.GetBlobUrlAsync(image.Name)
                };
                imageWithUrls.Add(imageVM);
            }

            roomDetailVM.ImagesUrls = imageWithUrls;
            return roomDetailVM;
        }

        //public async Task<List<RoomVM>> GetRoomsByHotelId(int hotelId)
        //{
        //    var datas = await _roomRepository.GetRoomsByHotelId(hotelId);

        //    return datas
        //        .Select(m => new RoomVM
        //        {
        //            Id = m.Id,
        //            Area = m.Area,
        //            Price = m.Price,
        //            BedCount = m.BedCount,
        //            GuestCapacity = m.GuestCapacity,
        //            HotelId = m.HotelId,
        //            MainImage = m.RoomImages.FirstOrDefault(m => m.IsMain).Name,
        //            Type = m.Type.ToString()
        //        })
        //        .GroupBy(r => new { r.GuestCapacity, r.BedCount }) 
        //        .Select(group =>
        //        {
        //            var room = group.First();
        //            return room;
        //        }).ToList();
        //}
        public async Task<List<RoomVM>> GetRoomsByHotelId(int hotelId)
        {
            var datas = await _roomRepository.GetRoomsByHotelId(hotelId);

            var roomVMs = new List<RoomVM>();

            foreach (var group in datas.GroupBy(r => new { r.GuestCapacity, r.BedCount }))
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
                    Count = group.Count()
                };

                roomVMs.Add(roomVM);
            }

            return roomVMs;
        }
    }
}
