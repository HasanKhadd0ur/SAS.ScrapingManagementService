using SAS.ScrapingManagementService.Application.DataSources.Common;
using SAS.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Application.Scrapers.Common
{
    public class ScrapingTaskMessage
    {
        public Guid Id { get; set; }
        public string Domain { get; set; }
        public string Platform { get; set; }
        public List<DataSourceDto> DataSources { get; set; }
        public ScrapingApproachDto ScrapingApproach { get; set; } = new();
        public int Limit { get; set; }
    }
    public class ScrapingApproachDto
    {
        public string Mode { get; set; } = "Scheduled";
        public string Platform { get; set; } = "Twitter";
        public string Name { get; set; } = "TwitterKeywordScraper";
    }

}
