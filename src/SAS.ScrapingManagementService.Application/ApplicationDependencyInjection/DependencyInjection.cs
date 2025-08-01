using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SAS.ScrapingManagementService.Application.Behaviors.LoggingBehavior;
using SAS.ScrapingManagementService.Application.Behaviors.ValidationBehavior;
using SAS.ScrapingManagementService.Application.Common.Mappings;
using SAS.ScrapingManagementService.Application.Contracts.Scheduling;
using SAS.ScrapingManagementService.Application.Settings.Services;
using System.Reflection;

namespace SAS.ScrapingManagementService.Application.ApplicationDependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddMyMediatR()
                .AddMappers();

            services.AddScoped<IPipelineConfigService, PipelineConfigService>();
            services.AddScoped<IBlockedTermsService, BlockedTermsService>();



            // Register orchestrator
            services.AddSingleton<SchedulerOrchestrator>(provider =>
            {
                var schedulers = provider.GetServices<IPlatformTaskScheduler>();
                return new SchedulerOrchestrator(schedulers);
            });

            return services;
        }

        #region Mediator
        private static IServiceCollection AddMyMediatR(this IServiceCollection services)
        {
            // Registers MediatR handlers from this assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Registers pipeline behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            
            // Registers FluentValidation validators from the current assembly
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
        #endregion Mediator


        #region Mappers 
        private static IServiceCollection AddMappers(this IServiceCollection services)
        {

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                
            });


            return services;
        }

        #endregion Mappers

    }
}

