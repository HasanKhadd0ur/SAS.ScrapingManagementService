using MediatR;
using Microsoft.Extensions.Logging;
using SAS.ScrapingManagementService.Application.Contracts.FeedBack;
using SAS.ScrapingManagementService.Application.Contracts.Scheduling;
using SAS.ScrapingManagementService.Application.ScrapingTasks.UseCases.Commands.CreateScrapingTask;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.SharedKernel.Repositories;

public partial class DataSourceDiscoveryEngine
{
    private readonly IEnumerable<IFeedbackDiscoveryStrategy> _strategies;
    private readonly IRepository<DataSource, Guid> _dataSourceRepo;
    private readonly IRepository<ScrapingDomain, Guid> _domainRepo;
    private readonly SchedulerOrchestrator _schedulerOrchestrator;
    private readonly IMediator _mediator;
    private readonly ILogger<DataSourceDiscoveryEngine> _logger;

    public DataSourceDiscoveryEngine(
        IEnumerable<IFeedbackDiscoveryStrategy> strategies,
        IRepository<DataSource, Guid> dataSourceRepo,
        IRepository<ScrapingDomain, Guid> domainRepo,
        SchedulerOrchestrator schedulerOrchestrator,
        IMediator mediator,
        ILogger<DataSourceDiscoveryEngine> logger)
    {
        _strategies = strategies;
        _dataSourceRepo = dataSourceRepo;
        _domainRepo = domainRepo;
        _schedulerOrchestrator = schedulerOrchestrator;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<List<DataSource>> DiscoverAndScheduleAsync(DiscoveryFeedbackDto feedback, CancellationToken ct)
    {
        var strategy = _strategies.FirstOrDefault(s => s.SupportsSource(feedback.Origin));

        if (strategy == null)
        {
            _logger.LogWarning("No discovery strategy for origin: {Origin}", feedback.Origin);
            return new List<DataSource>();
        }

        var newSources = await strategy.DiscoverAsync(feedback, ct);

        if (!newSources.Any())
        {
            _logger.LogInformation("No new data sources discovered from feedback.");
            return newSources;
        }

        await _dataSourceRepo.AddRangeAsync(newSources);
        _logger.LogInformation("Persisted {Count} new data sources.", newSources.Count);

        // For each new data source schedule scraping tasks immediately
        foreach (var ds in newSources)
        {
            await ScheduleScrapingTasksForDataSourceAsync(ds, ct);
        }

        return newSources;
    }

    private async Task ScheduleScrapingTasksForDataSourceAsync(DataSource dataSource, CancellationToken ct)
    {
        // Load domain fully with DataSources included
        var domain = await _domainRepo.GetByIdAsync(dataSource.DomainId);
        if (domain == null)
        {
            _logger.LogWarning("Domain {DomainId} not found for data source {DataSourceName}", dataSource.DomainId, dataSource.Name);
            return;
        }

        if (!_schedulerOrchestrator.TryGetScheduler(dataSource.Platform.Name, out var scheduler))
        {
            _logger.LogWarning("No scheduler found for platform {PlatformName}", dataSource.Platform.Name);
            return;
        }

        var scrapingTasks = await scheduler.ScheduleTasksAsync(domain);

        // Filter tasks including the new data source
        var relevantTasks = scrapingTasks
            .Where(t => t.DataSources.Any(ds => ds.Id == dataSource.Id))
            .ToList();

        foreach (var task in relevantTasks)
        {
            var command = new CreateScrapingTaskCommand
            {
                DomainId = domain.Id,
                DataSourceIds = task.DataSources.Select(ds => ds.Id).ToList(),
                ScrapingApproach = scheduler.GetApproach()
            };

            await _mediator.Send(command, ct);
            _logger.LogInformation("Scheduled scraping task for domain {DomainId} with new data source {DataSourceName}", domain.Id, dataSource.Name);
        }
    }
}
