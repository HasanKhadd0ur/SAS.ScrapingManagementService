using Ardalis.Result;
using AutoMapper;
using SAS.ScrapingManagementService.Application.Scrapers.Common;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.SharedKernel.CQRS.Queries;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Specification;

namespace SAS.ScrapingManagementService.Application.Scrapers.UseCases.Queries.GetAllScrapers
{
    public class GetAllScrapersQueryHandler : IQueryHandler<GetAllScrapersQuery, Result<List<ScraperDto>>>
    {
        private readonly IRepository<Scraper, Guid> _scraperRepo;
        private readonly IMapper _mapper;

        public GetAllScrapersQueryHandler(IRepository<Scraper, Guid> scraperRepo, IMapper mapper)
        {
            _scraperRepo = scraperRepo;
            _mapper = mapper;
        }

        public async Task<Result<List<ScraperDto>>> Handle(GetAllScrapersQuery request, CancellationToken cancellationToken)
        {
            var scrapers = await _scraperRepo.ListAsync(spec);

            var scraperDtos = _mapper.Map<List<ScraperDto>>(scrapers);

            return Result<List<ScraperDto>>.Success(scraperDtos);
        }
    }
}
