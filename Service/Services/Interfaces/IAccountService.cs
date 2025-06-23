using Service.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task AddRole(AddRoleVM model);
        Task RemoveRole(RemoveRoleVM model);
        Task Delete(string id);
    }
}
