using SAS.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Domain.DataSourceTypes.Entities
{
    public class DataSourceType : BaseEntity<Guid>
    {
        public string Name { get; set; }  // e.g. "Channel", "Group", "User", "Keyword"
    }
}
