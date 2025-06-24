using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IEmailTableService
    {
        Task<IEnumerable<Email>> GetAll();
        Task Add(Email email);
    }
}
