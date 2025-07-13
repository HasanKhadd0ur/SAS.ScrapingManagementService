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

                if (_settings.RunAtTimes?.Any() == true)
                {
                    // Parse scheduled times for today
                    var runTimes = _settings.RunAtTimes
                        .Select(t => DateTime.Today.Add(TimeSpan.Parse(t)))
                        .OrderBy(t => t)
                        .ToList();

                    // Find next run time after now
                    var nextRun = runTimes.FirstOrDefault(t => t > now);

                    if (nextRun == default)
                    {
                        // All today's run times passed, schedule earliest tomorrow
                        nextRun = runTimes.First().AddDays(1);
                    }

                    var timeToNextRun = nextRun - now;

                    // Check if current time is within 1 minute of any run time
                    var isOnSchedule = runTimes.Any(t => Math.Abs((t - now).TotalMinutes) < 1);

                    if (isOnSchedule)
                    {
                        _logger.LogInformation("Running scheduled scraping tasks at {Now}", now);
                        await RunSchedulingLogic(stoppingToken);

                        // Sleep at least 1 minute to avoid rerunning multiple times within the minute
                        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                    }
                    else
                    {
                        _logger.LogInformation("Not scheduled time yet. Sleeping until next run at {NextRun}", nextRun);
                        await Task.Delay(timeToNextRun, stoppingToken);
                    }
                }
                else
                {
                    // No scheduled times configured, run at fixed interval
                    _logger.LogInformation("No scheduled run times configured, running immediately and delaying for {Interval} minutes", _settings.IntervalMinutes);
                    await RunSchedulingLogic(stoppingToken);

                    await Task.Delay(TimeSpan.FromMinutes(_settings.IntervalMinutes), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while scheduling scraping tasks");
                // Avoid tight failure loop: delay before retrying
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
