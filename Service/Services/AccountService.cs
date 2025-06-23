using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Service.Services.Interfaces;
using Service.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task AddRole(AddRoleVM model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            await _userManager.AddToRoleAsync(user, model.Role);
        }

        public async Task Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task RemoveRole(RemoveRoleVM model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            await _userManager.RemoveFromRoleAsync(user, model.Role);
        }
    }
}
