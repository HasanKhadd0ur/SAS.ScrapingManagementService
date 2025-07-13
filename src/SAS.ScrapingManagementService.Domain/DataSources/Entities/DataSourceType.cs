using SAS.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Domain.DataSources.Entities
{
    public class DataSourceType : BaseEntity<Guid>
    {
        public string Name { get; set; }  // e.g. "Channel", "Group", "User", "Keyword"
    }
}
