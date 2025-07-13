using SAS.ScrapingManagementService.Application.Common;

namespace SAS.ScrapingManagementService.Application.DataSourceTypes.Common
{
    public class DataSourceTypeDto : BaseDTO<Guid>
    {
        public string Name { get; set; } 
    }
}
