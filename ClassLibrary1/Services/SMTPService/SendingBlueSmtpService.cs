using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Lesson1_BL.Services.SMTPService
{
    public class SendingBlueSmtpService : ISendingBlueSmtpService
    {
        private readonly SmtpConfiguration _smtpConfiguration;

        public SendingBlueSmtpService(IOptions<SmtpConfiguration> options)
        {
            _smtpConfiguration = options.Value;
        }

        public async Task SendMail(MailInfo mailInfo)
        {
            using (var SmtpServer = new SmtpClient
            {
                Host = _smtpConfiguration.Host,
                Port = _smtpConfiguration.Port,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    _smtpConfiguration.SenderMail,
                    _smtpConfiguration.SenderPassword),
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network
            })
            {
                var fromMessage = new MailAddress(
                    _smtpConfiguration.SenderMail,
                    _smtpConfiguration.SenderName);
                var toMessage = new MailAddress(
                    mailInfo.Email,
                    mailInfo.ClientName);
                using (MailMessage message = new MailMessage
                {
                    From = fromMessage,
                    Subject = mailInfo.Subject,
                    Body = mailInfo.Body
                })
                {
                    message.To.Add(toMessage);

                    await SmtpServer.SendMailAsync(message);
                }
            }
        }
    }
}
