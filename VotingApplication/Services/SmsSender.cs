using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(ApplicationUser user, string body);
    }

    public class SmsSender : ISmsSender
    {
        public Task SendSmsAsync(ApplicationUser user, string body)
        {
            throw new NotImplementedException();
        }
    }
}
