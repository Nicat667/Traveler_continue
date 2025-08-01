﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface ISettingRepository : IBaseRepository<Setting>
    {
        Task<Dictionary<string, string>> GetSettingAsync();
        Task<IEnumerable<Setting>> GetAllSettingAsync();
    }
}
