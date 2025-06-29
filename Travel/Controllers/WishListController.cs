using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Services;
using Service.Services.Interfaces;
using Service.ViewModels.WishListVM;

namespace Travel.Controllers
{
    public class WishListController : Controller
    {
        private readonly IWishListService _wishListService;
        private readonly UserManager<AppUser> _userManager;
        public WishListController(IWishListService wishListService, UserManager<AppUser> userManager)
        {
            _wishListService = wishListService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            await _wishListService.AddToSession(id);
            if(User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                WishListVM wishListVM = new WishListVM()
                {
                    HotelId = id,
                    UserId = user.Id
                };
                
                await _wishListService.Create(wishListVM);
            }
            //return Ok(); 
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetWishlistSession()
        {
            var wishlistJson = HttpContext.Session.GetString("wishlist");

            if (string.IsNullOrEmpty(wishlistJson))
            {
                return Json(new { message = "Wishlist is empty." });
            }

            var wishlist = JsonConvert.DeserializeObject<List<int>>(wishlistJson);

            return Json(wishlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int id)
        {
            //bool check = await _wishListService.IsInWishList(id);
            //if (User.Identity.IsAuthenticated)
            //{
            //    if (!check)
            //    {
            //        var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //        WishListVM wishListVM = new WishListVM()
            //        {
            //            HotelId = id,
            //            UserId = user.Id
            //        };

            //        await _wishListService.Create(wishListVM);
            //    }
            //    if(check) 
            //    {
            //        var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //        WishListVM wishListVM = new WishListVM()
            //        {
            //            HotelId = id,
            //            UserId = user.Id
            //        };
            //        await _wishListService.Delete(wishListVM);
            //    }
            //}


            //await _wishListService.AddOrRemove(id);
            //bool isInWishlist = await _wishListService.IsInWishList(id);

            bool isInWishlist = await _wishListService.ToggleAsync(id);

            return Ok(new { isInWishlist });
        }
    }
}
