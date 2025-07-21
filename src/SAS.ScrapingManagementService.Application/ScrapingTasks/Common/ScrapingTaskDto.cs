using SAS.ScrapingManagementService.Application.Common;
using SAS.ScrapingManagementService.Application.DataSources.Common;
using SAS.ScrapingManagementService.Application.ScrapingDomains.Common;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Application.ScrapingTasks.Common
{
    public class ScrapingTaskDto : BaseDTO<Guid>
        {
            public DateTime PublishedAt { get; set; }
            public DateTime? CompletedAt { get; set; }
            public Guid? ScraperId { get; set; }
            public Scraper? ScrapingExecutor { get; set; }
            public Guid DomainId { get; set; }
            public ScrapingDomainDto Domain { get; set; }
            public ICollection<DataSourceDto> DataSources { get; set; }
        }

}
