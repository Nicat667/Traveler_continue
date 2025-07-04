using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using Service.ViewModels;
using Service.ViewModels.Hotel;
using Service.ViewModels.HotelImages;

namespace Service.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomService _roomService;
        private readonly IBlobStorage _blobStorage;
        private readonly IHotelImageService _hotelImageService;
        public HotelService(IHotelRepository hotelRepository,
                            IRoomService roomService, 
                            IBlobStorage blobStorage, 
                            IHotelImageService hotelImageService)
        {
            _hotelRepository = hotelRepository;
            _roomService = roomService;
            _blobStorage = blobStorage;
            _hotelImageService = hotelImageService;
        }

        public async Task Create(HotelCreateVM model)
        {
            Hotel hotel = new Hotel()
            {
                Name = model.Name,
                Description = model.Description,
                Address = model.Address,
                SpaSauna = model.SpaSauna,
                StarCount = model.StarCount,
                AirConditioning = model.AirConditioning,
                AirportTransport = model.AirportTransport,
                CityId = model.CityId,
                FitnessCenter = model.FitnessCenter,
                Parking = model.Parking,
                Restaurant = model.Restaurant,
            };
            List<HotelImageCreateVM> images = new List<HotelImageCreateVM>();
            await _hotelRepository.CreateAsync(hotel);
            foreach (var item in model.Images)
            {
                HotelImageCreateVM image = new HotelImageCreateVM()
                {
                    IsMain = false,
                    Name = await _blobStorage.UploadAsync(item),
                    HotelId = hotel.Id,
                };
              
                images.Add(image);
            }
            images.FirstOrDefault().IsMain = true;
            
            await _hotelImageService.Create(images);
        }

        public async Task Delete(int id)
        {
            var data = await _hotelRepository.GetHotelById(id);
            if(data != null)
            {
                await _hotelRepository.DeleteAsync(data);
                foreach(var image in data.HotelImages)
                {
                    await _blobStorage.DeleteAsync(image.Name);
                }
            }
        }

        public async Task<IEnumerable<HotelVM>> GetAllHotel()
        {
            var datas = await _hotelRepository.GetAllHotel();
            List<HotelVM> result = new List<HotelVM>();
            foreach (var data in datas)
            {
                HotelVM hotelVM = new HotelVM()
                {
                    Id = data.Id,
                    Name = data.Name,
                    StarCount = data.StarCount,
                    HotelImageUrl = await _blobStorage.GetBlobUrlAsync(data.HotelImages.FirstOrDefault(x => x.IsMain == true).Name),
                    Address = data.Address,
                    MinPrice = data.Rooms.Any(r => r.HotelId == data.Id) ? data.Rooms.Where(r => r.HotelId == data.Id).Min(r => r.Price) : 0,
                    MaxPrice = data.Rooms.Any(r => r.HotelId == data.Id) ? data.Rooms.Where(r => r.HotelId == data.Id).Max(r => r.Price) : 0,
                    CommentCount = data.Comments.Where(x => x.HotelId == data.Id).Count(),
                    Rate = data.Comments.Any(c => c.HotelId == data.Id) ? data.Comments.Where(c => c.HotelId == data.Id).Sum(c => c.Rate) / (decimal)data.Comments.Count(c => c.HotelId == data.Id) : 5
                };
                result.Add(hotelVM);
            }
            return result;
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
                    //MainImage = m.HotelImages.FirstOrDefault(x => x.IsMain == true).Name,
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
            HotelDetailVM vm = new HotelDetailVM()
            {
                Id = data.Id,
                Name = data.Name,
                StarCount = data.StarCount,
                Address = data.Address,
                Comments = data.Comments.Where(m => m.HotelId == id),
                Rate = data.Comments.Any(c => c.HotelId == data.Id) ? data.Comments.Where(c => c.HotelId == data.Id).Sum(c => c.Rate) / (decimal)data.Comments.Count(c => c.HotelId == data.Id) : 5,
                Restaurant = data.Restaurant,
                AirConditioning = data.AirConditioning,
                AirportTransport = data.AirportTransport,
                SpaSauna = data.SpaSauna,
                FitnessCenter = data.FitnessCenter,
                Parking = data.Parking,
                Description = data.Description,
                CityId = data.CityId,
                Rooms = await _roomService.GetRoomsByHotelId(id)
            };
            List<HotelImageVM> urls = new List<HotelImageVM>();
            foreach(var image in data.HotelImages.Where(m => m.HotelId == id))
            {
                HotelImageVM hotelImageVM = new HotelImageVM()
                {
                    HotelId = image.HotelId,
                    Id = image.Id,
                    IsMain = image.IsMain,
                    Url = await _blobStorage.GetBlobUrlAsync(image.Name)
                };
                urls.Add(hotelImageVM);
            }
            vm.ImageUrls = urls;
            return vm;
        }

        public async Task<IEnumerable<HotelVM>> HotelFilter(FilterVM filter)
        {
            var datas = await _hotelRepository.GetAllHotel();
            List<HotelVM> result = new List<HotelVM>();
            foreach (var data in datas)
            {
                HotelVM hotelVM = new HotelVM()
                {
                    Id = data.Id,
                    Name = data.Name,
                    StarCount = data.StarCount,
                    HotelImageUrl = await _blobStorage.GetBlobUrlAsync(data.HotelImages.FirstOrDefault(x => x.IsMain == true).Name),
                    Address = data.Address,
                    MinPrice = data.Rooms.Any(r => r.HotelId == data.Id) ? data.Rooms.Where(r => r.HotelId == data.Id).Min(r => r.Price) : 0,
                    MaxPrice = data.Rooms.Any(r => r.HotelId == data.Id) ? data.Rooms.Where(r => r.HotelId == data.Id).Max(r => r.Price) : 0,
                    CommentCount = data.Comments.Where(x => x.HotelId == data.Id).Count(),
                    Rate = data.Comments.Any(c => c.HotelId == data.Id) ? data.Comments.Where(c => c.HotelId == data.Id).Sum(c => c.Rate) / (decimal)data.Comments.Count(c => c.HotelId == data.Id) : 5
                };

                result.Add(hotelVM);
            }

            return result.Where(m => m.MaxPrice <= filter.MaxValue && m.MinPrice >= filter.MinValue && (filter.StarCount == null || filter.StarCount.Contains(m.StarCount))); ;
        }

        public async Task<IEnumerable<HotelVM>> HotelFilterByCity(int id)
        {
            var datas = await _hotelRepository.GetAllHotel();
            List<HotelVM> result = new List<HotelVM>();
            foreach(var data in datas)
            {
                HotelVM hotelVM = new HotelVM()
                {
                    Id = data.Id,
                    Name = data.Name,
                    StarCount = data.StarCount,
                    HotelImageUrl = await _blobStorage.GetBlobUrlAsync(data.HotelImages.FirstOrDefault(x => x.IsMain == true).Name),
                    Address = data.Address,
                    MinPrice = data.Rooms.Any(r => r.HotelId == data.Id) ? data.Rooms.Where(r => r.HotelId == data.Id).Min(r => r.Price) : 0,
                    MaxPrice = data.Rooms.Any(r => r.HotelId == data.Id) ? data.Rooms.Where(r => r.HotelId == data.Id).Max(r => r.Price) : 0,
                    CommentCount = data.Comments.Where(x => x.HotelId == data.Id).Count(),
                    Rate = data.Comments.Any(c => c.HotelId == data.Id) ? data.Comments.Where(c => c.HotelId == data.Id).Sum(c => c.Rate) / (decimal)data.Comments.Count(c => c.HotelId == data.Id) : 5,
                    CityId = data.Id,
                };

                result.Add(hotelVM);
            }
            return result.Where(x => x.CityId == id);
        }

        public async Task<IEnumerable<HotelVM>> Search(SearchVM query)
        {
            var datas = await _hotelRepository.Search();
            
            if (query.CityName != null)
            {
                datas = datas.Where(m => m.City.Name == query.CityName);
            }

            var hotels = datas.Where(hotel =>
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
                Address = m.Address,
                MinPrice = m.Rooms.Any(r => r.HotelId == m.Id) ? m.Rooms.Where(r => r.HotelId == m.Id).Min(r => r.Price) : 0,
                MaxPrice = m.Rooms.Any(r => r.HotelId == m.Id) ? m.Rooms.Where(r => r.HotelId == m.Id).Max(r => r.Price) : 0,
                CommentCount = m.Comments.Where(x => x.HotelId == m.Id).Count(),
                Rate = m.Comments.Any(c => c.HotelId == m.Id) ? m.Comments.Where(c => c.HotelId == m.Id).Sum(c => c.Rate) / (decimal)m.Comments.Count(c => c.HotelId == m.Id) : 5
            }).ToList();

            foreach (var hotel in hotels)
            {
                var hotelEntity = datas.FirstOrDefault(x => x.Id == hotel.Id);
                var mainImage = hotelEntity?.HotelImages.FirstOrDefault(i => i.IsMain);

                if (mainImage != null)
                {
                    hotel.HotelImageUrl = await _blobStorage.GetBlobUrlAsync(mainImage.Name);
                }
            }
            return hotels;
        }

        public async Task Update(int id, HotelEditVM model)
        {
            var existData = await _hotelRepository.GetHotelById(id);
            existData.Name = model.Name;
            existData.Description = model.Description;
            existData.Address = model.Address;
            existData.AirportTransport = model.AirportTransport;
            existData.Restaurant = model.Restaurant;
            existData.AirConditioning = model.AirConditioning;
            existData.CityId = model.CityId;
            existData.FitnessCenter = model.FitnessCenter;
            existData.SpaSauna = model.SpaSauna;
            existData.Parking = model.Parking;
            existData.StarCount = model.StarCount;

            await _hotelRepository.UpdateAsync(existData);
        }
    }
}