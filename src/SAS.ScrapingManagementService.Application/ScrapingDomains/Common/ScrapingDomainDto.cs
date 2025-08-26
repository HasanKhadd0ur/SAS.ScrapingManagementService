using SAS.ScrapingManagementService.Application.Common;
using SAS.ScrapingManagementService.Application.DataSources.Common;

namespace SAS.ScrapingManagementService.Application.ScrapingDomains.Common
{
    public class ScrapingDomainDto :BaseDTO <Guid>
    {
        public string NormalisedName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<DataSourceDto> DataSources { get; set; }
    }

}
