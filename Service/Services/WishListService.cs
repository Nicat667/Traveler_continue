using Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using Service.ViewModels.WishListVM;
using System.Text;

namespace Service.Services
{
    public class WishListService : IWishListService
    {
        private readonly IWishListRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        public WishListService(IWishListRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _contextAccessor = httpContextAccessor;
        }

        public async Task AddToSession(int id)
        {
            var wishlistJson = _contextAccessor.HttpContext.Session.GetString("wishlist");
            List<int> list = null;
            if (wishlistJson != null)
            {
                list = JsonConvert.DeserializeObject<List<int>>(wishlistJson);
            }
            else
            {
                list = new List<int>();
            }
            if(!list.Contains(id))
            {
                list.Add(id);
            }
            _contextAccessor.HttpContext.Session.SetString("wishlist", JsonConvert.SerializeObject(list));
        }


        public async Task Create(WishListVM vm)
        {
            await _repository.CreateAsync(new WishList
            {
                HotelId = vm.HotelId,
                AppUserId = vm.UserId,
            });
        }
    }
}
