using Ardalis.Result;
using AutoMapper;
using MediatR;
using SAS.ScrapingManagementService.Application.DataSources.Common;
using SAS.ScrapingManagementService.Application.DataSources.UseCases.Queries;
using SAS.ScrapingManagementService.Application.ScrapingDomains.Common;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.DomainErrors;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Queries.GetDomainById
{
    public class GetDomainByIdQueryHandler : IRequestHandler<GetDomainByIdQuery, Result<ScrapingDomainDto>>
    {
        private readonly IRepository<ScrapingDomain, Guid> _repo;
        private readonly IMapper _mapper;

        public GetDomainByIdQueryHandler(
            IRepository<ScrapingDomain, Guid> domainRepo,
            IMapper mapper)
        {
            _repo = domainRepo;
            _mapper = mapper;
        }

        public async Task<Result<ScrapingDomainDto>> Handle(GetDomainByIdQuery request, CancellationToken cancellationToken)
        {
            var domain = await _repo.GetByIdAsync(request.Id);

            if (domain is null)
                return Result.Invalid(ScrapingDomainErrors.UnExistDomain);

            var dto = _mapper.Map<ScrapingDomainDto>(domain);

            return Result.Success(dto);
        }
    }
}
