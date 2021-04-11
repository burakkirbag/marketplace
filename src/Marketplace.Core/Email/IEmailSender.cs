using System.Threading.Tasks;

namespace Marketplace.Email
{
    public interface IEmailSender
    {
        void Send(EmailMessage message);

        Task SendAsync(EmailMessage message);
    }
}
