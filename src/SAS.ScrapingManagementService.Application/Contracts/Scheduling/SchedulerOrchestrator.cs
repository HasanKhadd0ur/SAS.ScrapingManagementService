using SAS.ScrapingManagementService.Domain.DataSources.Entities;

namespace SAS.ScrapingManagementService.Application.Contracts.Scheduling
{
    public class SchedulerOrchestrator
    {
        private readonly Dictionary<string, IPlatformTaskScheduler> _schedulers;

        public SchedulerOrchestrator(IEnumerable<IPlatformTaskScheduler> schedulers)
        {
            _schedulers = schedulers.ToDictionary(s => s.PlatformName, StringComparer.OrdinalIgnoreCase);
        }

        public bool TryGetScheduler(string platformName, out IPlatformTaskScheduler scheduler)
        {
            return _schedulers.TryGetValue(platformName, out scheduler);
        }

    }
}
