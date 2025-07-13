using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Domain.ScrapingDomains.Repositories
{
    public interface IScrapingDomainRepository : IRepository<ScrapingDomain, Guid>
    {

    }
}
