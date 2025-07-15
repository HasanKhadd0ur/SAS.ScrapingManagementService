using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAS.ScrapingManagementService.Domain.DataSources.Repositories;
using SAS.ScrapingManagementService.Domain.Settings.Repositories;
using SAS.ScrapingManagementService.Infrastructure.Persistence.AppDataContext;
using SAS.ScrapingManagementService.Infrastructure.Persistence.Repositories.Base;
using SAS.ScrapingManagementService.Infrastructure.Persistence.Repositories.DataSources;
using SAS.ScrapingManagementService.Infrastructure.Persistence.UoW;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Utilities;

namespace SAS.ScrapingManagementService.Infrastructure.Persistence.DependencyInjection
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDataContext(configuration)
                .AddRepositories()
                .AddUOW();

            return services;
        }




        #region Register UOW 

        private static IServiceCollection AddUOW(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
        #endregion Register UOW 

        #region Register Repositories
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IDataSourceRepository, DataSourceRepository>();
            services.AddScoped<IDataSourceTypeRepository, DataSourceTypeRepository>();
            services.AddScoped<IPipelineRepository, PipelineRepository>();

            return services;

        }


        #endregion Register Repositoryies

        #region Register Data context 
        private static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;

        }

        #endregion Register Data Context 
    }
}
