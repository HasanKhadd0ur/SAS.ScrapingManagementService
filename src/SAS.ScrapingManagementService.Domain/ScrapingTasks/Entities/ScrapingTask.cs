using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Domain.Tasks.Entities
{
    public class ScrapingTask : BaseEntity<Guid>
    {
        public DateTime PublishedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public Guid? ScraperId { get; set; }
        public Scraper? ScrapingExecutor { get; set; }
        public Guid DomainId { get; set; }
        public ScrapingDomain Domain { get; set; }
        public ICollection<DataSource> DataSources { get; set; }
    }

}
