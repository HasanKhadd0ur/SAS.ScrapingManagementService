using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SAS.ScrapingManagementService.Application.Contracts.Scheduling;
using SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.CreateScrapingTask;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.ScrapingManagementService.Infrastructure.Services.BackgroundServices;
using SAS.ScrapingManagementService.SharedKernel.Repositories;

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
                var nextRun = _settings.RunAtTimes
                    .Select(t => DateTime.Today.Add(TimeSpan.Parse(t)))
                    .Select(t => t <= now ? t.AddDays(1) : t)
                    .OrderBy(t => t)
                    .First();

                var delay = nextRun - now;
                _logger.LogInformation("Next scheduled run at {NextRun}", nextRun);
                await Task.Delay(delay, stoppingToken);
            }
            else
            {
                await Task.Delay(TimeSpan.FromMinutes(_settings.IntervalMinutes), stoppingToken);
            }
        }

    }
    private async Task RunSchedulingLogic(CancellationToken stoppingToken)
    {
        using var scope = _provider.CreateScope();
        var domainRepo = scope.ServiceProvider.GetRequiredService<IRepository<ScrapingDomain, Guid>>();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var domains = await domainRepo.ListAsync();

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
                        DataSourceIds = task.DataSources.Select(ds => ds.Id).ToList()
                    };

                    await mediator.Send(cmd, stoppingToken);
                }
            }
        }
    }

}