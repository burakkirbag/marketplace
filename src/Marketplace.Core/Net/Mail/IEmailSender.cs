using System.Threading.Tasks;

namespace Marketplace.Net.Mail
{
    public interface IEmailSender
    {
        void Send(EmailMessage message);

        Task SendAsync(EmailMessage message);
    }
}
