using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace VotingApplication.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(ApplicationUser user, string subject, string body);
    }

    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(ApplicationUser user, string subject, string body)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(userName: "votingnotification6@gmail.com", password: "Team6Admin")
            };

            var message = new MailMessage
            {
                From = new MailAddress("votingnotification6@gmail.com", "Voting Online"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(user.Email, user.UserName));
            await smtp.SendMailAsync(message);
        }
    }
}
