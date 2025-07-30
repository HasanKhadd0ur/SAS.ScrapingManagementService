using SAS.ScrapingManagementService.Domain.DataSourceTypes.Entities;
using SAS.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Domain.DataSourceTypes.Repositories
{
    public interface IDataSourceTypesRepository : IRepository<DataSourceType, Guid>
    {

        Task<DataSourceType> GetByNameAsync(string name);
    }

}
