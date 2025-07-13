using Ardalis.Result;
using AutoMapper;
using Confluent.Kafka.Admin;
using MediatR;
using SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.UpdateDataSource;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.DomainErrors;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Commands.UpdateScrapingDomain
{
    public sealed class UpdateScrapingDomainCommandHandler : IRequestHandler<UpdateScrapingDomainCommand, Result>
    {
        private readonly IRepository<DataSource, Guid> _dataSourceRepo;
        private readonly IRepository<ScrapingDomain, Guid> _domainRepo;
        private readonly IRepository<Platform, Guid> _platformRepo;
        private readonly IMapper _mapper;

        public UpdateScrapingDomainCommandHandler(
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

        public async Task<Result> Handle(UpdateScrapingDomainCommand request, CancellationToken cancellationToken)
        {
            
            var domain = await _domainRepo.GetByIdAsync(request.Id);
            if (domain is null)
                return Result.Invalid(ScrapingDomainErrors.UnExistDomain);


            _mapper.Map(request, domain); // Update primitive properties
            
            await _domainRepo.UpdateAsync(domain);

            return Result.Success();
        }
    }
}
