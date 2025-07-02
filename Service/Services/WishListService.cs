using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly IHotelService _hotelService;
        public WishListService(IWishListRepository repository, 
                               IHttpContextAccessor httpContextAccessor, 
                               UserManager<AppUser> userManager,
                               IHotelService hotelService)
        {
            _repository = repository;
            _contextAccessor = httpContextAccessor;
            _userManager = userManager;
            _hotelService = hotelService;
        }

        public async Task AddOrRemove(int id)
        {
            var wishlistJson = _contextAccessor.HttpContext.Session.GetString("wishlist");
            List<int> list = new List<int>();
            if (wishlistJson != null)
            {
                list = JsonConvert.DeserializeObject<List<int>>(wishlistJson);
                if (list.Contains(id))
                {
                    list.Remove(id);
                }
                else
                {
                    list.Add(id);  
                }
            }
            else
            {
                list.Add(id);
            }
            _contextAccessor.HttpContext.Session.SetString("wishlist", JsonConvert.SerializeObject(list));
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
            var datas = await _repository.GetAllAsync();
            var check = datas.FirstOrDefault(m=>m.AppUserId == vm.UserId && m.HotelId == vm.HotelId);
            if(check == null)
            {
                await _repository.CreateAsync(new WishList
                {
                    HotelId = vm.HotelId,
                    AppUserId = vm.UserId,
                });
            }
           
        }

        public async Task Delete(WishListVM vm)
        {
            WishList wishList = new WishList()
            {
                AppUserId = vm.UserId,
                HotelId = vm.HotelId,
            };
            await _repository.DeleteAsync(wishList);
        }

        public async Task<IEnumerable<HotelsWLVM>> GetAll()
        {
            var user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
            var wishlistItems = await _repository.GetByUserId(user.Id);
            List<HotelsWLVM> result = new List<HotelsWLVM>();
            if(wishlistItems != null)
            {
                foreach (var item in wishlistItems)
                {
                    var temp = await _hotelService.GetHotelDetail(item.HotelId);
                    result.Add(new HotelsWLVM
                    {
                        Id = item.HotelId,
                        Name = temp.Name,
                        StarCount = temp.StarCount,
                        MainImageUrl = temp.ImageUrls.FirstOrDefault(m => m.IsMain).Url
                    });
                }
            }
            return result;
        }

        public async Task<int> GetCount()
        {
            string items = _contextAccessor.HttpContext.Session.GetString("wishlist");
            if (items == null)
            {
                return 0;
            }
            var list = JsonConvert.DeserializeObject<List<int>>(items);
            return list.Count();
        }

        public async Task<bool> IsInWishList(int id)
        {
            var wishlistJson = _contextAccessor.HttpContext.Session.GetString("wishlist");
            List<int> list = null;
            if (wishlistJson != null)
            {
                list = JsonConvert.DeserializeObject<List<int>>(wishlistJson);
                if (list.Contains(id))
                {
                    return true;
                }
            }
            return false;
        }


        public async Task<bool> ToggleAsync(int hotelId)
        {
            var httpCtx = _contextAccessor.HttpContext;
            var session = httpCtx.Session;
            var user = httpCtx.User;

          
            var json = session.GetString("wishlist");
            var list = string.IsNullOrEmpty(json)
                        ? new List<int>()
                        : JsonConvert.DeserializeObject<List<int>>(json);

            bool added;
            if (list.Contains(hotelId))
            {
                list.Remove(hotelId);
                added = false;      
            }
            else
            {
                list.Add(hotelId);
                added = true;    
            }
            session.SetString("wishlist", JsonConvert.SerializeObject(list));

   
            if (user.Identity.IsAuthenticated == true)
            {
                var appUser = await _userManager.GetUserAsync(user);

          
                var datas = await _repository.GetAllAsync();
                var entity = datas.FirstOrDefault(x => x.AppUserId == appUser.Id && x.HotelId == hotelId);

                if (added)
                {
                    if (entity == null)
                    {
                        await _repository.CreateAsync(new WishList
                        {
                            AppUserId = appUser.Id,
                            HotelId = hotelId
                        });
                    }
                }
                else      
                {
                    if (entity != null)
                    {
                        await _repository.DeleteAsync(entity);  
                    }
                }
            }

            return added;
        }
    }
}
