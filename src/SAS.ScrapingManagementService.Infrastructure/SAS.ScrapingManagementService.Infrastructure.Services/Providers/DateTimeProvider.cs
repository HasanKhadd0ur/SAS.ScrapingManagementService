using SAS.ScrapingManagementService.Application.Contracts.Providers;

namespace SAS.ScrapingManagementService.Infrastructure.Services.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
