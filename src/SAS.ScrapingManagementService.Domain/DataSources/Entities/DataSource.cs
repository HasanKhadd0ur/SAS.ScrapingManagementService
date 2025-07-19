using SAS.ScrapingManagementService.Domain.Platforms.Entities;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Domain.DataSources.Entities
{
    public class DataSource : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public string Target { get; set; } // Maybe a keyword or a channel or a userame
        
        public Guid PlatformId { get; set; } //  Belong to a platofrm
        public Platform Platform { get; set; } //  Belong to a platofrm
        
        public Guid DomainId { get; set; }
        public ScrapingDomain Domain { get; set; } //  Belong to a Domain

        public Guid DataSourceTypeId { get; set; }
        public DataSourceType DataSourceType { get; set; }  

    }
}
