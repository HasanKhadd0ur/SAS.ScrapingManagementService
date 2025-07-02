// Application/Common/Mappings/MappingProfile.cs
using AutoMapper;
using SAS.ScrapingManagementService.Application.DataSources.Common;
using SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.AddDataSource;
using SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.UpdateDataSource;
using SAS.ScrapingManagementService.Application.DataSourceTypes.Common;
using SAS.ScrapingManagementService.Application.Platforms.Common;
using SAS.ScrapingManagementService.Application.ScrapingDomains.Common;
using SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Commands.CreateScrapingDomain;
using SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Commands.UpdateScrapingDomain;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;

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
            
        }
    }
}
