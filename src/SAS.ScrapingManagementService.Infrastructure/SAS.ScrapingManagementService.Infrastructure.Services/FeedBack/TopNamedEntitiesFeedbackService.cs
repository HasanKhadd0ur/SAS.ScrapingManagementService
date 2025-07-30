using Microsoft.Extensions.Logging;
using SAS.ScrapingManagementService.Application.Contracts.FeedBack;
using SAS.ScrapingManagementService.Application.Contracts.Messaging;
using System.Text.Json;

public partial class DataSourceDiscoveryEngine
{
    public class TopNamedEntitiesFeedbackService 
    {
        private readonly IMessageConsumer _messageConsumer;
        private readonly DataSourceDiscoveryEngine _discoveryEngine;
        private readonly ILogger<TopNamedEntitiesFeedbackService> _logger;
        private readonly string _topicName = "scraping-feedback";

        public TopNamedEntitiesFeedbackService(
            IMessageConsumer messageConsumer,
            DataSourceDiscoveryEngine discoveryEngine,
            ILogger<TopNamedEntitiesFeedbackService> logger)
        {
            _messageConsumer = messageConsumer;
            _discoveryEngine = discoveryEngine;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting TopNamedEntitiesFeedbackService, listening on topic {Topic}", _topicName);

            await foreach (var message in _messageConsumer.ConsumeAsync<string>(_topicName, cancellationToken))
            {
                try
                {
                    var feedback = JsonSerializer.Deserialize<DiscoveryFeedbackDto>(message);

                    if (feedback == null)
                    {
                        _logger.LogWarning("Received null or invalid feedback message.");
                        continue;
                    }

                    var newDataSources = await _discoveryEngine.DiscoverAndScheduleAsync(feedback, cancellationToken);

                    _logger.LogInformation("Processed feedback from origin {Origin}: Created {Count} new data sources.",
                        feedback.Origin, newDataSources.Count);
                }
                catch (JsonException jsonEx)
                {
                    _logger.LogError(jsonEx, "JSON deserialization error for feedback message: {Message}", message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing feedback message.");
                }
            }

            _logger.LogInformation("TopNamedEntitiesFeedbackService stopped listening on topic {Topic}", _topicName);
        }
    }
}
