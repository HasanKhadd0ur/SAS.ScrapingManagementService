using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SAS.ScrapingManagementService.Application.Contracts.Scheduling;
using SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.CreateScrapingTask;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.ScrapingManagementService.Infrastructure.Services.BackgroundServices;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Specification;

public partial class ScrapingTaskSchedulerService : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly ILogger<ScrapingTaskSchedulerService> _logger;
    private readonly SchedulerOrchestrator _orchestrator;
    private readonly ScrapingSchedulerSettings _settings;

    public ScrapingTaskSchedulerService(
        IServiceProvider provider,
        ILogger<ScrapingTaskSchedulerService> logger,
        SchedulerOrchestrator orchestrator,
        IOptions<ScrapingSchedulerSettings> settings)
    {
        _provider = provider;
        _logger = logger;
        _orchestrator = orchestrator;
        _settings = settings.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var now = DateTime.Now;
                bool shouldRun = false;

                // Check if now matches any configured RunAtTimes (like "03:00", "15:45", etc.)
                if (_settings.RunAtTimes?.Any() == true)
                {
                    shouldRun = _settings.RunAtTimes
                        .Select(t => TimeSpan.Parse(t))
                        .Any(ts => Math.Abs((now.TimeOfDay - ts).TotalMinutes) < 1);
                }

                // Always run per interval (default: every minute)
                if (_settings.IntervalMinutes > 0 && now.Minute % _settings.IntervalMinutes == 0)
                {
                    shouldRun = true;
                }

                if (shouldRun)
                {
                    _logger.LogInformation("Running scheduled scraping tasks at {Now}", now);
                    await RunSchedulingLogic(stoppingToken);
                }
                else
                {
                    _logger.LogInformation("No run triggered at {Now}", now);
                }

                // Wait for 1 minute or configured interval
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while scheduling scraping tasks");
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }


    private async Task RunSchedulingLogic(CancellationToken stoppingToken)
    {
        using var scope = _provider.CreateScope();
        var domainRepo = scope.ServiceProvider.GetRequiredService<IRepository<ScrapingDomain, Guid>>();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var spec = new BaseSpecification<ScrapingDomain>();
        spec.AddInclude(e => e.DataSources);
        spec.IncludeStrings.Add("DataSources.Platform");
        spec.IncludeStrings.Add("DataSources.DataSourceType");
        var domains = await domainRepo.ListAsync(spec);

        foreach (var domain in domains)
        {
            var platformGroupedSources = domain.DataSources
                .GroupBy(ds => ds.Platform.Name, StringComparer.OrdinalIgnoreCase);

            foreach (var platformGroup in platformGroupedSources)
            {
                if (!_orchestrator.TryGetScheduler(platformGroup.Key, out var scheduler))
                    continue;

                var generatedTasks = await scheduler.ScheduleTasksAsync(domain);

                foreach (var task in generatedTasks)
                {
                    var cmd = new CreateScrapingTaskCommand
                    {
                        DomainId = domain.Id,
                        DataSourceIds = task.DataSources.Select(ds => ds.Id).ToList(),
                        ScrapingApproach = scheduler.GetApproach()
                    };

                    var result = await mediator.Send(cmd, stoppingToken);
                    _logger.LogInformation("Created scraping task with result: {Result}", result);
                }
            }
        }
    }
}
