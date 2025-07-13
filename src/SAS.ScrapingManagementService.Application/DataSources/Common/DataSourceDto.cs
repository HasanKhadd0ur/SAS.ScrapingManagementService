using SAS.ScrapingManagementService.Application.Common;
using SAS.ScrapingManagementService.Application.DataSourceTypes.Common;
using SAS.ScrapingManagementService.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Application.DataSources.Common
{
    public class DataSourceDto :BaseDTO<Guid>
    {
        public string Name { get; set; }
        public string Target { get; set; }
        public Guid DomainId { get; set; }
        public Guid PlatformId { get; set; }
        public int Limit { get; set; } = 1;
        public DataSourceTypeDto DataSourceType { get; set; }
    }
}