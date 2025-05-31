using Ardalis.Result;
using AutoMapper;
using MediatR;
using SAS.ScrapingManagementService.Application.Common;
using SAS.ScrapingManagementService.Application.DataSources.Common;
using SAS.ScrapingManagementService.Application.ScrapingDomains.Common;
using SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Queries.GetAllDomain;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.ScrapingManagementService.SharedKernel.Repositories;
using SAS.ScrapingManagementService.SharedKernel.Specification;

namespace SAS.ScrapingManagementService.Application.DataSources.UseCases.Queries.GetAllDataSources
{
    public class GetAllDomainsQueryHandler : IRequestHandler<GetAllDomainsQuery, Result<IEnumerable<ScrapingDomainDto>>>
    {
        private readonly IRepository<ScrapingDomain, Guid> _domainRepo;
        private readonly IMapper _mapper;

        public GetAllDomainsQueryHandler(IRepository<ScrapingDomain, Guid> domainRepo, IMapper mapper)
        {
            _domainRepo = domainRepo;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ScrapingDomainDto>>> Handle(GetAllDomainsQuery request, CancellationToken cancellationToken)
        {
            
            // Build spec inline
            var spec = new BaseSpecification<ScrapingDomain>();

            spec.ApplyOptionalPagination(request.PageSize, request.PageNumber);

            var entities = await _domainRepo.ListAsync(spec);

            var dtoList = _mapper.Map<IEnumerable<ScrapingDomainDto>>(entities);


            return Result.Success(dtoList);
        }
    }
}
