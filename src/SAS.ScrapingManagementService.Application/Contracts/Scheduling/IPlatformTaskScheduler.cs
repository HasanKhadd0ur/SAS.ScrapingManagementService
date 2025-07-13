using SAS.ScrapingManagementService.Application.Scrapers.Common;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.ScrapingManagementService.Domain.Tasks.Entities;

public interface IPlatformTaskScheduler
{
    string PlatformName { get; }
    Task<List<ScrapingTask>> ScheduleTasksAsync(ScrapingDomain domain);
    ScrapingApproachDto GetApproach();

}
