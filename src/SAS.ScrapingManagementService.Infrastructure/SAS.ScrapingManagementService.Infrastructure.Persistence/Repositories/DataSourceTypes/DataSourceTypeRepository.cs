using SAS.ScrapingManagementService.Domain.DataSourceTypes.Entities;
using SAS.ScrapingManagementService.Domain.DataSourceTypes.Repositories;
using SAS.ScrapingManagementService.Infrastructure.Persistence.AppDataContext;
using SAS.ScrapingManagementService.Infrastructure.Persistence.Repositories.Base;
using SAS.SharedKernel.Specification;

namespace SAS.ScrapingManagementService.Infrastructure.Persistence.Repositories.DataSourceTypes
{
    public class DataSourceTypesRepository : BaseRepository<DataSourceType, Guid>, IDataSourceTypesRepository
    {

        public DataSourceTypesRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<DataSourceType> GetByNameAsync(string name)
        {
            var spec = new BaseSpecification<DataSourceType>(e => e.Name == name);
            var result = await ListAsync(spec);
            return result.FirstOrDefault();
        }
    }
}
