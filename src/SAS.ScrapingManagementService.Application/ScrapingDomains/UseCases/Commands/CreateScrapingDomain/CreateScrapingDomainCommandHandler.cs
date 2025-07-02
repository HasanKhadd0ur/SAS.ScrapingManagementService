using Ardalis.Result;
using AutoMapper;
using MediatR;
using SAS.ScrapingManagementService.Application.Contracts.Providers;
using SAS.ScrapingManagementService.Domain.DataSources.DomainErrors;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.DomainErrors;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.ScrapingManagementService.SharedKernel.Repositories;
using SAS.ScrapingManagementService.SharedKernel.Specification;

namespace SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Commands.CreateScrapingDomain
{
    public sealed class CreateScrapingDomainCommandHandler : IRequestHandler<CreateScrapingDomainCommand, Result<Guid>>
    {
        private readonly IRepository<ScrapingDomain, Guid> _domainRepo;
        private readonly IIdProvider _idProvider;
        private readonly IMapper _mapper;

        public CreateScrapingDomainCommandHandler(
            IRepository<ScrapingDomain, Guid> domainRepo,
            IIdProvider idProvider,
            IMapper mapper)
        {
            _domainRepo = domainRepo;
            _idProvider = idProvider;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateScrapingDomainCommand request, CancellationToken cancellationToken)
        {
            var normalized = request.NormalisedName.Trim().ToLowerInvariant();

            var spec = new BaseSpecification<ScrapingDomain>(x => x.NormalisedName.ToLower() == normalized);
            var existing = await _domainRepo.ListAsync(spec);

            if (existing.Any())
            {
                return Result.Invalid(ScrapingDomainErrors.AlreadyExists(request.Name));
            }

            var id = _idProvider.GenerateId<ScrapingDomain>();
            var domain = _mapper.Map<ScrapingDomain>(request);
            domain.Id = id;
            domain.NormalisedName = normalized;

            await _domainRepo.AddAsync(domain);

            return Result.Success(id);
        }
    }
}
