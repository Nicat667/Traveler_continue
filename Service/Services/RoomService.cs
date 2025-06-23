using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories.Interfaces;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using Service.ViewModels.Room;

namespace Service.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationService _reservationService;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        public RoomService(IRoomRepository roomRepository, IReservationService reservationService, IEmailService emailService, UserManager<AppUser> userManager)
        {
            _roomRepository = roomRepository;
            _reservationService = reservationService;
            _emailService = emailService;
            _userManager = userManager;
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
            return datas.Select(r => new RoomVM
            {
                Area = r.Area,
                Price = r.Price,
                BedCount = r.BedCount,
                GuestCapacity = r.GuestCapacity,
                HotelId = r.HotelId,
                MainImage = r.RoomImages.FirstOrDefault(m => m.IsMain == true).Name,
                Type = r.Type.ToString()
            });
        }

        public async Task<RoomDetailVM> GetRoomById(int id)
        {
            var data = await _roomRepository.GetRoomById(id);
            var options = await GetRoomsByHotelId(data.HotelId);
            return new RoomDetailVM
            {
                Area = data.Area,
                Price = data.Price,
                BedCount = data.BedCount,
                GuestCapacity = data.GuestCapacity,
                HotelId = data.HotelId,
                Id = data.Id,
                Images = data.RoomImages,
                Type = data.Type.ToString(),
                Description = data.Description,
                Options = options.Where(r => r.HotelId == data.HotelId && r.Price != data.Price && r.Id != data.Id).ToList()
            };
        }

        public async Task<List<RoomVM>> GetRoomsByHotelId(int hotelId)
        {
            var datas = await _roomRepository.GetRoomsByHotelId(hotelId);

            return datas
                .Select(m => new RoomVM
                {
                    Id = m.Id,
                    Area = m.Area,
                    Price = m.Price,
                    BedCount = m.BedCount,
                    GuestCapacity = m.GuestCapacity,
                    HotelId = m.HotelId,
                    MainImage = m.RoomImages.FirstOrDefault(m => m.IsMain).Name,
                    Type = m.Type.ToString()
                })
                .GroupBy(r => new { r.GuestCapacity, r.BedCount }) 
                .Select(group =>
                {
                    var room = group.First();
                    return room;
                }).ToList();
        }

    }
}
