using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Services.Interfaces;
using Service.ViewModels.Account;
using System.Threading.Tasks;

namespace Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountService _accountService;
        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IAccountService accountService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<UserVM> usersWithRoles = new List<UserVM>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                UserVM userVM = new UserVM()
                {
                    Id = user.Id,
                    Fullname = user.FullName,
                    Username = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList(),
                };
                usersWithRoles.Add(userVM);
            }
            return View(usersWithRoles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(AddRoleVM model)
        {
            await _accountService.AddRole(model);
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRole(RemoveRoleVM model)
        {
            await _accountService.RemoveRole(model);
            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            var availableRoles = allRoles.Except(userRoles);

            return Json(availableRoles);
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();;

            return Json(allRoles);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var user =await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _accountService.Delete(id);
            return Ok();
        }

        // *** For adding roles to database ***
        //[HttpGet]
        //public async Task<IActionResult> CreateRole()
        //{
        //    foreach (var role in Enum.GetValues(typeof(Roles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }
        //    }
        //    return Ok();
        //}
    }
}
