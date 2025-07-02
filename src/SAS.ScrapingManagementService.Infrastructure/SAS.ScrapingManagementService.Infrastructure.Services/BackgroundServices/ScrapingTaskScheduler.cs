using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SAS.ScrapingManagementService.Application.Contracts.Scheduling;
using SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.CreateScrapingTask;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.ScrapingManagementService.Infrastructure.Services.BackgroundServices;
using SAS.ScrapingManagementService.SharedKernel.Repositories;
using SAS.ScrapingManagementService.SharedKernel.Specification;

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
                await RunSchedulingLogic(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while scheduling scraping tasks");
            }

            if (_settings.RunAtTimes?.Any() == true)
            {
                var now = DateTime.Now;

                // Parse run times to today's datetime and then tomorrow if already passed
                var runTimes = _settings.RunAtTimes
                    .Select(t => DateTime.Today.Add(TimeSpan.Parse(t)))
                    .OrderBy(t => t)
                    .ToList();

                // Find the next scheduled time after now
                var nextRun = runTimes.FirstOrDefault(t => t > now);

                if (nextRun == default)
                {
                    // All times passed today, pick the earliest one tomorrow
                    nextRun = runTimes.First().AddDays(1);
                }

                var timeToNextRun = nextRun - now;

                // Check if now is close to any scheduled time (within some threshold, e.g. 1 minute)
                var isOnSchedule = runTimes.Any(t => Math.Abs((t - now).TotalMinutes) < 1);

                if (!isOnSchedule)
                {
                    // If not exactly on scheduled time, run immediately next loop
                    _logger.LogInformation("Current time not on schedule, running immediately.");
                    await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                }
                else
                {
                    _logger.LogInformation("Next scheduled run at {NextRun}", nextRun);
                    await Task.Delay(timeToNextRun, stoppingToken);
                }
            }
            else
            {
                // No runAtTimes configured - just delay interval minutes
                await Task.Delay(TimeSpan.FromMinutes(_settings.IntervalMinutes), stoppingToken);
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
                    Console.WriteLine(result);
                }
            }
        }
    }

}