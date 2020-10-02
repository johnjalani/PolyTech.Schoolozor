using System.Threading.Tasks;

namespace Schoolozor.Shared
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
