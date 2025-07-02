using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Domain.DataSources.Repositories
{
    public interface IDataSourceTypeRepository : IRepository<DataSourceType, Guid>
    {

        Task<DataSourceType> GetByNameAsync(string name);
    }

}
