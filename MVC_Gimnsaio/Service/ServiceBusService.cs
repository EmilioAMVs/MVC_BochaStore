using Azure.Messaging.ServiceBus;
using System.Threading.Tasks;

namespace MVC_BOCHA_STORE.Service
{

    public class ServiceBusService : IServiceBusService
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _productsSender;
        private readonly ServiceBusSender _suppliersSender;
        private readonly ServiceBusSender _brandsSender;

        public ServiceBusService(string connectionString, string productsQueueName, string suppliersQueueName, string brandsQueueName)
        {
            _client = new ServiceBusClient(connectionString);
            _productsSender = _client.CreateSender(productsQueueName);
            _suppliersSender = _client.CreateSender(suppliersQueueName);
            _brandsSender = _client.CreateSender(brandsQueueName);
        }

        public async Task SendMessageAsync(string messageBody, QueueType queueType)
        {
            ServiceBusSender sender = queueType switch
            {
                QueueType.Products => _productsSender,
                QueueType.Suppliers => _suppliersSender,
                QueueType.Brands => _brandsSender,
                _ => throw new ArgumentException("Invalid queue type", nameof(queueType))
            };

            ServiceBusMessage message = new ServiceBusMessage(messageBody);
            await sender.SendMessageAsync(message);
        }

        public Task SendMessageAsync(string messageBody)
        {
            throw new NotImplementedException();
        }
    }

    public enum QueueType
    {
        Products,
        Suppliers,
        Brands
    }
}
