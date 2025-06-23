using Service.ViewModels.FAQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IFAQService
    {
        Task<IEnumerable<FAQVM>> GetAll();
        Task Create(CreateVM model);
        Task<FAQVM> GetById(int id);
        Task Delete(int id);
        Task Edit(int id, EditVM model);
    }
}
