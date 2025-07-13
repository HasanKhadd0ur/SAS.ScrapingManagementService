using SAS.ScrapingManagementService.Application.Scrapers.Common;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.ScrapingManagementService.Domain.Tasks.Entities;

public class TelegramTaskScheduler : IPlatformTaskScheduler
{
    public string PlatformName => "Telegram";

    public async Task<List<ScrapingTask>> ScheduleTasksAsync(ScrapingDomain domain)
    {
        // group by Telegram channels, date, etc.
        var relevantSources = domain.DataSources
            .Where(ds => ds.Platform.Name == "Telegram")
            .ToList();

        var tasks = new List<ScrapingTask>
        {
            new ScrapingTask
            {
                Id = Guid.NewGuid(),
                PublishedAt = DateTime.UtcNow,
                Domain = domain,
                DataSources = relevantSources
            }
        };

        return tasks;
    }
    public ScrapingApproachDto GetApproach()
    {
        return new ScrapingApproachDto
        {
            Mode = "Scheduled",
            Platform = "Telegram",
            Name = "TelegramWebScraper"
        };
    }
}
