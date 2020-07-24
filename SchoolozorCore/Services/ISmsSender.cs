using System.Threading.Tasks;

namespace SchoolozorCore.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
