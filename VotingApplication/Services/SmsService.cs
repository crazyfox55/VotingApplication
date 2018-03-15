using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.Services
{
    public interface ISmsService
    {
        Task SendSmsAsync(ApplicationUser user, string body);
    }

    public class SmsService : ISmsService
    {
        public Task SendSmsAsync(ApplicationUser user, string body)
        {
            throw new NotImplementedException();
        }
    }
}
