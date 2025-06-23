using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using Service.ViewModels.FAQ;

namespace Service.Services
{
    public class FAQService : IFAQService
    {
        private readonly IFAQRepository _fAQRepository;
        public FAQService(IFAQRepository fAQRepository)
        {
            _fAQRepository = fAQRepository;
        }

        public async Task Create(CreateVM model)
        {
            await _fAQRepository.CreateAsync(new FAQ
            {
                Question = model.Question,
                Answer = model.Answer,
            });
        }

        public async Task Delete(int id)
        {
            var data = await _fAQRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _fAQRepository.DeleteAsync(data);
            }
            
        }

        public async Task<FAQVM> GetById(int id)
        {
            var data = await _fAQRepository.GetByIdAsync(id);
            return new FAQVM
            {
                Question = data.Question,
                Answer = data.Answer,
            };
        }

        public async Task Edit(int id, EditVM model)
        {
            var data = await _fAQRepository.GetByIdAsync(id);
            if(data != null)
            {
                data.Answer = model.Answer;
                data.Question = model.Question;
            }
            await _fAQRepository.UpdateAsync(data);
        }

        public async Task<IEnumerable<FAQVM>> GetAll()
        {
            var datas = await _fAQRepository.GetAllAsync();
            return datas.Select(x => new FAQVM
            {
                Id = x.Id,
                Answer = x.Answer,
                Question = x.Question,
            });
        }
    }
}
