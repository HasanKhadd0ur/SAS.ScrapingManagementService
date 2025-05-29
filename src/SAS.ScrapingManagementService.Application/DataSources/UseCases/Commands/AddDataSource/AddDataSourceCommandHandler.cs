using Ardalis.Result;
using AutoMapper;
using MediatR;
using SAS.ScrapingManagementService.Domain.DataSources.DomainErrors;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.ScrapingManagementService.SharedKernel.Repositories;
using SAS.ScrapingManagementService.Application.Contracts.Providers;

namespace SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.AddDataSource
{
    public sealed class AddDataSourceCommandHandler : IRequestHandler<AddDataSourceCommand, Result<Guid>>
    {
        private readonly IRepository<ScrapingDomain, Guid> _domainRepo;
        private readonly IRepository<Platform, Guid> _platformRepo;
        private readonly IRepository<DataSource, Guid> _dataSourceRepo;
        private readonly IIdProvider _idProvider;
        private readonly IMapper _mapper;

        public AddDataSourceCommandHandler(
            IRepository<ScrapingDomain, Guid> domainRepo,
            IRepository<Platform, Guid> platformRepo,
            IRepository<DataSource, Guid> dataSourceRepo,
            IIdProvider idProvider,
            IMapper mapper)
        {
            _domainRepo = domainRepo;
            _platformRepo = platformRepo;
            _dataSourceRepo = dataSourceRepo;
            _idProvider = idProvider;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(AddDataSourceCommand request, CancellationToken cancellationToken)
        {
            var domain = await _domainRepo.GetByIdAsync(request.DomainId);
            if (domain is null)
                return Result.Invalid(ScrapingDomainErrors.UnExistDomain);

            var platform = await _platformRepo.GetByIdAsync(request.PlatformId);
            if (platform is null)
                return Result.Invalid(PlatformErrors.UnExistPlatform);

            var dataSource = _mapper.Map<DataSource>(request);

            dataSource.Id = _idProvider.GenerateNewId();
            dataSource.Domain = domain;
            dataSource.Platform = platform;

            await _dataSourceRepo.AddAsync(dataSource);

            return Result.Success(dataSource.Id);
        }
    }
}
