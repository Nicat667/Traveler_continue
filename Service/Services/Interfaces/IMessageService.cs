using Service.ViewModels.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IMessageService
    {
        Task Create(MessageCreateVM model);
        Task<IEnumerable<MessageVM>> GetAll();
        Task<MessageDetailVM> GetDetail(int id);
        Task Delete(int id);
        Task Update(int id);
        Task Answer(int id, AnswerVM answer);
    }
}
