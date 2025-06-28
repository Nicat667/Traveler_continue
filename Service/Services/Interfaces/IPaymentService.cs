using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreateCheckoutSessionAsync(long amount, string successUrl, string cancelUrl, string userEmail);
    }
}
