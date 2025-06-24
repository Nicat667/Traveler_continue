using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EmailTableService : IEmailTableService
    {
        private readonly IEmailRepository _emailRepository;
        public EmailTableService(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }
        public async Task Add(Email email)
        {
            await _emailRepository.CreateAsync(email);
        }

        public async Task<IEnumerable<Email>> GetAll()
        {
            return await _emailRepository.GetAllAsync();
        }
    }
}
