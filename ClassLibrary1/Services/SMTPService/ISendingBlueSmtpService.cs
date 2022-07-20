using System.Threading.Tasks;

namespace Lesson1_BL.Services.SMTPService
{
    public interface ISendingBlueSmtpService
    {
        Task SendMail(MailInfo mailInfo);
    }
}