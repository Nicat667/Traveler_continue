using Repository.Repositories.Interfaces;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using Service.ViewModels;
using Service.ViewModels.Hotel;

namespace Service.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomService _roomService;
        public HotelService(IHotelRepository hotelRepository, IRoomService roomService)
        {
            _hotelRepository = hotelRepository;
            _roomService = roomService;
        }

        public async Task<IEnumerable<HotelVM>> GetAllHotel()
        {
            var datas = await _hotelRepository.GetAllHotel();
            return datas.Select(m => new HotelVM
            {
                Id = m.Id,
                Name = m.Name,
                StarCount = m.StarCount,
                MainImage = m.HotelImages.FirstOrDefault(x=>x.IsMain == true).Name,
                Address = m.Address,
                MinPrice = m.Rooms.Any(r => r.HotelId == m.Id) ? m.Rooms.Where(r => r.HotelId == m.Id).Min(r => r.Price) : 0,
                MaxPrice = m.Rooms.Any(r => r.HotelId == m.Id) ? m.Rooms.Where(r => r.HotelId == m.Id).Max(r => r.Price) : 0,
                CommentCount = m.Comments.Where(x=>x.HotelId == m.Id).Count(),
                Rate = m.Comments.Any(c => c.HotelId == m.Id) ? m.Comments.Where(c => c.HotelId == m.Id).Sum(c => c.Rate) / (decimal)m.Comments.Count(c => c.HotelId == m.Id) : 5
            });
        }

        public async Task<PaginateResponse<HotelVM>> GetAllHotelsPaginated(int page, int take = 6)
        {
            var hotels = await _hotelRepository.GetAllHotel();
            var paginatedDatas = await _hotelRepository.GetAllPaginated(page, take);
            int count = hotels.Count();
            int pageSize = (int)Math.Ceiling((double)count/take);
            var response = new PaginateResponse<HotelVM>()
            {
                Datas = paginatedDatas.Select(m => new HotelVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    StarCount = m.StarCount,
                    MainImage = m.HotelImages.FirstOrDefault(x => x.IsMain == true).Name,
                    Address = m.Address,
                    MinPrice = m.Rooms.Any(r => r.HotelId == m.Id) ? m.Rooms.Where(r => r.HotelId == m.Id).Min(r => r.Price) : 0,
                    CommentCount = m.Comments.Count(x => x.HotelId == m.Id),
                    Rate = m.Comments.Any(c => c.HotelId == m.Id)
                        ? m.Comments.Where(c => c.HotelId == m.Id).Sum(c => c.Rate) / (decimal)m.Comments.Count(c => c.HotelId == m.Id)
                        : 5
                }).ToList(),
                PageCount = pageSize
            };

            return response;
        }

        public async Task<HotelDetailVM> GetHotelDetail(int id)
        {
            var data = await _hotelRepository.GetHotelById(id);
            return new HotelDetailVM
            {
                Id = data.Id,
                Name = data.Name,
                StarCount = data.StarCount,
                Address = data.Address,
                Images = data.HotelImages.Where(m => m.HotelId == id),
                Comments = data.Comments.Where(m => m.HotelId == id),
                Rate = data.Comments.Any(c => c.HotelId == data.Id) ? data.Comments.Where(c => c.HotelId == data.Id).Sum(c => c.Rate) / (decimal)data.Comments.Count(c => c.HotelId == data.Id) : 5,
                Restaurant = data.Restaurant,
                AirConditioning = data.AirConditioning,
                AirportTransport = data.AirportTransport,
                SpaSauna = data.SpaSauna,
                FitnessCenter = data.FitnessCenter,
                Parking = data.Parking,
                Description = data.Description,
                Rooms = await _roomService.GetRoomsByHotelId(id)
            };
        }

        public async Task<IEnumerable<HotelVM>> HotelFilter(FilterVM filter)
        {
            var datas = await _hotelRepository.GetAllHotel();
            var filteredDatas = datas.Select(m => new HotelVM
            {
                Id = m.Id,
                Name = m.Name,
                StarCount = m.StarCount,
                MainImage = m.HotelImages.FirstOrDefault(x => x.IsMain == true).Name,
                Address = m.Address,
                MinPrice = m.Rooms.Any(r => r.HotelId == m.Id) ? m.Rooms.Where(r => r.HotelId == m.Id).Min(r => r.Price) : 0,
                MaxPrice = m.Rooms.Any(r => r.HotelId == m.Id) ? m.Rooms.Where(r => r.HotelId == m.Id).Max(r => r.Price) : 0,
                CommentCount = m.Comments.Where(x => x.HotelId == m.Id).Count(),
                Rate = m.Comments.Any(c => c.HotelId == m.Id) ? m.Comments.Where(c => c.HotelId == m.Id).Sum(c => c.Rate) / (decimal)m.Comments.Count(c => c.HotelId == m.Id) : 5
            }).Where(m => m.MaxPrice <= filter.MaxValue && m.MinPrice >= filter.MinValue && (filter.StarCount == null || filter.StarCount.Contains(m.StarCount)));
            return filteredDatas;
        }

        public async Task<IEnumerable<HotelVM>> HotelFilterByCity(int id)
        {
            var datas = await _hotelRepository.GetAllHotel();
            var result = datas.Select(m => new HotelVM
            {
                Id = m.Id,
                Name = m.Name,
                StarCount = m.StarCount,
                MainImage = m.HotelImages.FirstOrDefault(x => x.IsMain == true).Name,
                Address = m.Address,
                MinPrice = m.Rooms.Any(r => r.HotelId == m.Id) ? m.Rooms.Where(r => r.HotelId == m.Id).Min(r => r.Price) : 0,
                MaxPrice = m.Rooms.Any(r => r.HotelId == m.Id) ? m.Rooms.Where(r => r.HotelId == m.Id).Max(r => r.Price) : 0,
                CommentCount = m.Comments.Where(x => x.HotelId == m.Id).Count(),
                CityId = m.CityId,
                Rate = m.Comments.Any(c => c.HotelId == m.Id) ? m.Comments.Where(c => c.HotelId == m.Id).Sum(c => c.Rate) / (decimal)m.Comments.Count(c => c.HotelId == m.Id) : 5
            }).Where(x => x.CityId == id);
            return result;
        }

        public async Task<IEnumerable<HotelVM>> Search(SearchVM query)
        {
            var datas = await _hotelRepository.Search();
            
            if (query.CityName != null)
            {
                datas = datas.Where(m => m.City.Name == query.CityName);
            }
            return datas.Where(hotel =>
            {
                var availableRooms = hotel.Rooms
                    .Where(r => !r.Reservations.Any(res =>
                        query.StartDate < res.EndDate &&
                        query.EndDate > res.StartDate))
                    .ToList();

                var validRoomTypeGroup = availableRooms
                    .GroupBy(r => r.Type)
                    .FirstOrDefault(group =>
                        group.Count() >= query.RoomCount &&
                        group.OrderByDescending(r => r.GuestCapacity)
                             .Take(query.RoomCount)
                             .Sum(r => r.GuestCapacity) >= query.GuestCount);

                return validRoomTypeGroup != null;
            }).Select(m => new HotelVM
            {
                Id = m.Id,
                Name = m.Name,
                StarCount = m.StarCount,
                MainImage = m.HotelImages.FirstOrDefault(x => x.IsMain == true).Name,
                Address = m.Address,
                MinPrice = m.Rooms.Any(r => r.HotelId == m.Id) ? m.Rooms.Where(r => r.HotelId == m.Id).Min(r => r.Price) : 0,
                MaxPrice = m.Rooms.Any(r => r.HotelId == m.Id) ? m.Rooms.Where(r => r.HotelId == m.Id).Max(r => r.Price) : 0,
                CommentCount = m.Comments.Where(x => x.HotelId == m.Id).Count(),
                Rate = m.Comments.Any(c => c.HotelId == m.Id) ? m.Comments.Where(c => c.HotelId == m.Id).Sum(c => c.Rate) / (decimal)m.Comments.Count(c => c.HotelId == m.Id) : 5
            });
        }

    }
}