using Schoolozor.Shared;
using System.Threading.Tasks;

namespace SchoolozorCore.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
