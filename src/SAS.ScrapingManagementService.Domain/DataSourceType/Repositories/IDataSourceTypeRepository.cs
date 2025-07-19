using SAS.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Domain.DataSourceType.Repositories
{
    public interface IDataSourceTypeRepository : IRepository<DataSourceType, Guid>
    {

        Task<DataSourceType> GetByNameAsync(string name);
    }

}
