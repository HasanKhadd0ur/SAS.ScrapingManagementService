using SAS.ScrapingManagementService.Application.Scrapers.Common;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;

public interface IPlatformTaskScheduler
{
    string PlatformName { get; }
    Task<List<ScrapingTask>> ScheduleTasksAsync(ScrapingDomain domain);
    ScrapingApproachDto GetApproach();

}
