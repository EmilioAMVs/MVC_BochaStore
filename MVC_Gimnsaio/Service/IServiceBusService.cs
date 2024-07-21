using MVC_BOCHA_STORE.Models;

namespace MVC_BOCHA_STORE.Service
{
    public interface IServiceBusService
    {
        Task SendMessageAsync(string messageBody, QueueType queueType);

    }
}
