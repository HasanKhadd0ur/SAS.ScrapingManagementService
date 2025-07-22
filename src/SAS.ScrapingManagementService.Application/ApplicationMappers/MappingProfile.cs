// Application/Common/Mappings/MappingProfile.cs
using AutoMapper;
using SAS.ScrapingManagementService.Application.DataSources.Common;
using SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.AddDataSource;
using SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.UpdateDataSource;
using SAS.ScrapingManagementService.Application.DataSourceTypes.Common;
using SAS.ScrapingManagementService.Application.Platforms.Common;
using SAS.ScrapingManagementService.Application.Scrapers.Common;
using SAS.ScrapingManagementService.Application.ScrapingDomains.Common;
using SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Commands.CreateScrapingDomain;
using SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Commands.UpdateScrapingDomain;
using SAS.ScrapingManagementService.Application.ScrapingTasks.Common;
using SAS.ScrapingManagementService.Application.Settings.Common;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.DataSourceTypes.Entities;
using SAS.ScrapingManagementService.Domain.Platforms.Entities;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.ScrapingManagementService.Domain.Settings.Entities;
using SAS.ScrapingManagementService.Domain.Tasks.Entities;
using static SAS.ScrapingManagementService.Application.Settings.Common.PipelineConfigDto;

namespace SAS.ScrapingManagementService.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ============ Platforms =============
            CreateMap<Platform, PlatformDto>().ReverseMap();

            // ============ DataSources ===========
            CreateMap<DataSource, DataSourceDto>().ReverseMap();
            CreateMap<AddDataSourceCommand, DataSource>();
            CreateMap<UpdateDataSourceCommand, DataSource>();

            // ============ ScrapingDomais ===========
            CreateMap<ScrapingDomain, ScrapingDomainDto>().ReverseMap();
            CreateMap<CreateScrapingDomainCommand, ScrapingDomain>();
            CreateMap<UpdateScrapingDomainCommand, ScrapingDomain>();
            
            // ============ DataSourceTypes ===========
            CreateMap<DataSourceType, DataSourceTypeDto>().ReverseMap();
         
            CreateMap<PipelineConfig, PipelineConfigDto>().ReverseMap();
            CreateMap<PipelineStage, PipelineStageDto>().ReverseMap();

            CreateMap<Scraper, ScraperDto>().ReverseMap();

            CreateMap<ScrapingTask, ScrapingTaskDto>().ReverseMap();
            CreateMap<BlockedTerm, BlockedTermDto>().ReverseMap();
        }
    }
}
