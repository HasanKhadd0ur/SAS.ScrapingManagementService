namespace SAS.ScrapingManagementService.Application.Contracts.Messaging
{
    public interface IMessageConsumer
    {
        /// <summary>
        /// Streams messages as raw strings from the specified topic.
        /// </summary>
        IAsyncEnumerable<TValue> ConsumeAsync<TValue>(string topic, CancellationToken cancellationToken);
    }
}
