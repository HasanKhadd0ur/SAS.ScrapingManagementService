using Ardalis.Result;
using AutoMapper;
using MediatR;
using SAS.ScrapingManagementService.Domain.DataSources.DomainErrors;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.ScrapingManagementService.SharedKernel.Repositories;
using SAS.ScrapingManagementService.Application.Contracts.Providers;
using SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.AddDataSource;

namespace SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Commands.CreateScrapingDomain
{
    public sealed class CreateScrapingDomainCommandHandler : IRequestHandler<CreateScrapingDomainCommand, Result<Guid>>
    {
        private readonly IRepository<ScrapingDomain, Guid> _domainRepo;
        private readonly IRepository<Platform, Guid> _platformRepo;
        private readonly IRepository<DataSource, Guid> _dataSourceRepo;
        private readonly IIdProvider _idProvider;
        private readonly IMapper _mapper;

        public CreateScrapingDomainCommandHandler(
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

        public async Task<Result<Guid>> Handle(CreateScrapingDomainCommand request, CancellationToken cancellationToken)
        {   
            return Result.Success(dataSource.Id);
        }
    }
}
