using System.Threading.Tasks;

namespace SAS.ScrapingManagementService.Application.Contracts.Messaging
{
    public interface IMessageProducerService

    {
        Task ProduceAsync<TValue>(string topic, TValue message);
    }
}
