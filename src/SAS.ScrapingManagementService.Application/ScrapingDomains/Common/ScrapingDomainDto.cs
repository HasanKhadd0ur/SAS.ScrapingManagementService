using SAS.ScrapingManagementService.Application.Common;

namespace SAS.ScrapingManagementService.Application.ScrapingDomains.Common
{
    public class ScrapingDomainDto :BaseDTO <Guid>
    {
        public string NormalisedName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

}