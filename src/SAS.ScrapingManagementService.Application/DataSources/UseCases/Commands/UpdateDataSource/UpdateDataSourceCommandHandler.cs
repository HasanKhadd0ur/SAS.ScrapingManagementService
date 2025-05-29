using Ardalis.Result;
using AutoMapper;
using MediatR;
using SAS.ScrapingManagementService.Domain.DataSources.DomainErrors;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.ScrapingManagementService.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.UpdateDataSource
{
    public sealed class UpdateDataSourceCommandHandler : IRequestHandler<UpdateDataSourceCommand, Result>
    {
        private readonly IRepository<DataSource, Guid> _dataSourceRepo;
        private readonly IRepository<ScrapingDomain, Guid> _domainRepo;
        private readonly IRepository<Platform, Guid> _platformRepo;
        private readonly IMapper _mapper;

        public UpdateDataSourceCommandHandler(
            IRepository<DataSource, Guid> dataSourceRepo,
            IRepository<ScrapingDomain, Guid> domainRepo,
            IRepository<Platform, Guid> platformRepo,
            IMapper mapper)
        {
            _dataSourceRepo = dataSourceRepo;
            _domainRepo = domainRepo;
            _platformRepo = platformRepo;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateDataSourceCommand request, CancellationToken cancellationToken)
        {
            var dataSource = await _dataSourceRepo.GetByIdAsync(request.Id);
            if (dataSource is null)
                return Result.NotFound(DataSourceErrors.NotFound);

            var domain = await _domainRepo.GetByIdAsync(request.DomainId);
            if (domain is null)
                return Result.Invalid(ScrapingDomainErrors.UnExistDomain);

            var platform = await _platformRepo.GetByIdAsync(request.PlatformId);
            if (platform is null)
                return Result.Invalid(PlatformErrors.UnExistPlatform);

            _mapper.Map(request, dataSource); // Update primitive properties
            dataSource.Domain = domain;
            dataSource.Platform = platform;

            await _dataSourceRepo.UpdateAsync(dataSource);

            return Result.Success();
        }
    }
}
