using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.DataSources.Repositories;
using SAS.ScrapingManagementService.Infrastructure.Persistence.AppDataContext;
using SAS.ScrapingManagementService.Infrastructure.Persistence.Repositories.Base;
using SAS.ScrapingManagementService.SharedKernel.Specification;

namespace SAS.ScrapingManagementService.Infrastructure.Persistence.Repositories.DataSources
{
    public class DataSourceTypeRepository : BaseRepository<DataSourceType, Guid>, IDataSourceTypeRepository
    {

        public DataSourceTypeRepository(AppDbContext context) : base(context)
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