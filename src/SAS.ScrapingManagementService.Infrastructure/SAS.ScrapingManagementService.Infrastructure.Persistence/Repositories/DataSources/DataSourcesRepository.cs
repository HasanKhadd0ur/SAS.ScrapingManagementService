using Microsoft.EntityFrameworkCore;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.DataSources.Repositories;
using SAS.ScrapingManagementService.Infrastructure.Persistence.AppDataContext;
using SAS.ScrapingManagementService.Infrastructure.Persistence.Repositories.Base;

namespace SAS.ScrapingManagementService.Infrastructure.Persistence.Repositories.DataSources
{
    public class DataSourceRepository : BaseRepository<DataSource, Guid>, IDataSourceRepository
    {

        public DataSourceRepository(AppDbContext context) : base(context)
        {
        }
        
    }
}
