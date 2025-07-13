using SAS.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Domain.DataSources.Entities
{
    public class Platform : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
