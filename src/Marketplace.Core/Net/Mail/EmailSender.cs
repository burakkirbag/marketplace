using System;
using System.Threading.Tasks;

namespace Marketplace.Net.Mail
{
    public class EmailSender : IEmailSender
    {
        public void Send(EmailMessage message)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(EmailMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
