using System.Threading.Tasks;

namespace Schoolozor.Shared
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
