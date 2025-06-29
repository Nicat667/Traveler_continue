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
        public WishListService(IWishListRepository repository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _contextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task AddOrRemove(int id)
        {
            //var user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
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

            // 1️⃣  ─── Session ────────────────────────────────────────────────
            var json = session.GetString("wishlist");
            var list = string.IsNullOrEmpty(json)
                        ? new List<int>()
                        : JsonConvert.DeserializeObject<List<int>>(json);

            bool added;
            if (list.Contains(hotelId))
            {
                list.Remove(hotelId);
                added = false;           // end state = NOT in wishlist
            }
            else
            {
                list.Add(hotelId);
                added = true;            // end state = IN wishlist
            }
            session.SetString("wishlist", JsonConvert.SerializeObject(list));

            // 2️⃣  ─── Database (only if signed‑in) ───────────────────────────
            if (user.Identity?.IsAuthenticated == true)
            {
                var appUser = await _userManager.GetUserAsync(user);

                // fetch once, so we work with a tracked entity
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
                else       // removed
                {
                    if (entity != null)
                    {
                        await _repository.DeleteAsync(entity);   // tracked → OK
                    }
                }
            }

            return added;
        }
    }
}
