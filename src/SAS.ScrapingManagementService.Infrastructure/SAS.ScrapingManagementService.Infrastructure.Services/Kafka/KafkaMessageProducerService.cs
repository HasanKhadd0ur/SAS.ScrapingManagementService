using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SAS.ScrapingManagementService.Application.Contracts.Messaging;
using System.Text.Json;

public class KafkaMessageProducerService : IMessageProducerService
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaMessageProducerService> _logger;

    public KafkaMessageProducerService(IOptions<KafkaSettings> kafkaSettings, ILogger<KafkaMessageProducerService> logger)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = kafkaSettings.Value.BootstrapServers
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
        _logger = logger;
    }

    public async Task ProduceAsync<TValue>(string topic, TValue message)
    {
        try
        {
            var key = Guid.NewGuid().ToString(); // Optional: use consistent key if partitioning is needed
            var value = JsonSerializer.Serialize(message);

            var msg = new Message<string, string> { Key = key, Value = value };

            var deliveryResult = await _producer.ProduceAsync(topic, msg);

            _logger.LogInformation("Delivered message to topic {Topic} at offset {Offset}", deliveryResult.Topic, deliveryResult.Offset);
        }
        catch (ProduceException<string, string> ex)
        {
            _logger.LogError(ex, "Kafka delivery failed: {Reason}", ex.Error.Reason);
        }
    }
}