using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using Service.ViewModels.Messages;

namespace Service.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IEmailService _emailService;
        public MessageService(IMessageRepository messageRepository, IEmailService emailService)
        {
            _messageRepository = messageRepository;
            _emailService = emailService;
        }

        public async Task Answer(int id, AnswerVM answer)
        {
            var data = await _messageRepository.GetByIdAsync(id);
            _emailService.Send(data.Email, "Support", answer.Answer);
        }

        public async Task Create(MessageCreateVM model)
        {
            await _messageRepository.CreateAsync(new Message
            {
                Text = model.Text,
                Phone = model.Phone,
                Email = model.Email,
                Name = model.Name,
                Created = DateTime.Now,
                IsRead = false,
            });

        }

        public async Task Delete(int id)
        {
            var data = await _messageRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _messageRepository.DeleteAsync(data);
            }
        }

        public async Task<IEnumerable<MessageVM>> GetAll()
        {
            var datas = await _messageRepository.GetAllAsync();
            return datas.OrderByDescending(x => x.Created).Select(x => new MessageVM
            {
                Id = x.Id,
                Name = x.Name,
                Time = DateTime.UtcNow - x.Created.ToUniversalTime(),
                IsRead = x.IsRead,
            });
        }

        public async Task<MessageDetailVM> GetDetail(int id)
        {
            var data = await _messageRepository.GetByIdAsync(id);
            return new MessageDetailVM
            {
                Name = data.Name,
                Text = data.Text,
                Phone = data.Phone,
                Email = data.Email,
                Created = data.Created,
            };
        }

        public async Task Update(int id)
        {
            var data = await _messageRepository.GetByIdAsync(id);
            data.IsRead = true;
            await _messageRepository.UpdateAsync(data);
        }
    }
}
