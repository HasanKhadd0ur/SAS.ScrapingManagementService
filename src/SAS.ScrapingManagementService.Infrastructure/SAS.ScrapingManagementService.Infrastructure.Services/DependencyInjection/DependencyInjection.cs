using Microsoft.Extensions.DependencyInjection;
using SAS.ScrapingManagementService.Application.Contracts.Providers;
using SAS.ScrapingManagementService.Infrastructure.Services.BackgroundServices;
using SAS.ScrapingManagementService.Infrastructure.Services.Providers;
using Microsoft.Extensions.Options;
using SAS.ScrapingManagementService.Application.Contracts.Scheduling;
using Microsoft.Extensions.Configuration;
using SAS.ScrapingManagementService.Application.Contracts.Messaging;

namespace SAS.ScrapingManagementService.Infrastructure.Services.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureSevices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddBackgroundServices(configuration)
                .AddCronJobs()
                .AddServices(configuration);

            return services;
        }

        #region Add Servcies 
        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Id Provider 
            services.AddSingleton<IIdProvider, IdProvider>();
            
            // Register Datetime Provider
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            // Register Kafka Producer
            services.AddSingleton<IMessageProducerService, KafkaMessageProducerService>();


            // Register platform schedulers
            services.AddSingleton<IPlatformTaskScheduler, TelegramTaskScheduler>();

            services.Configure<ScrapingSchedulerSettings>(
                    configuration.GetSection("ScrapingScheduler"));

            
            return services;
        }

        #endregion Add Servcies 

        #region Background jobs 
        private static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<ScrapingTaskSchedulerService>();

            return services;
        }

        #endregion Background jobs 

        #region Cron Jobs
        private static IServiceCollection AddCronJobs(this IServiceCollection services)
        {

            return services;

        }
        #endregion
    }
}
