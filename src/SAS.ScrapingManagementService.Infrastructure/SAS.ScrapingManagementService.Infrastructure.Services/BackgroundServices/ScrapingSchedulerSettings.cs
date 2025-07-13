namespace SAS.ScrapingManagementService.Infrastructure.Services.BackgroundServices
{
    public class ScrapingSchedulerSettings
    {
        public int IntervalMinutes { get; set; } = 60;
        public List<string> RunAtTimes { get; set; } = new();
    }
}
