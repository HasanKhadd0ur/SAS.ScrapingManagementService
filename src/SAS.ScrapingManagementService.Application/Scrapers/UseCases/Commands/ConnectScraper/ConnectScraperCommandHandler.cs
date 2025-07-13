using Ardalis.Result;
using MediatR;
using SAS.ScrapingManagementService.Application.Contracts.Providers;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Specification;

namespace SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.ConnectScraper
{
    public class ConnectScraperCommandHandler : IRequestHandler<ConnectScraperCommand, Result<Guid>>
    {
        private readonly IRepository<Scraper, Guid> _scraperRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IIdProvider _idProvider;

        public ConnectScraperCommandHandler(
            IRepository<Scraper, Guid> scraperRepository,
            IDateTimeProvider dateTimeProvider,
            IIdProvider idProvider)
        {
            _scraperRepository = scraperRepository;
            _dateTimeProvider = dateTimeProvider;
            _idProvider = idProvider;
        }

        public async Task<Result<Guid>> Handle(ConnectScraperCommand request, CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<Scraper>(s =>
                s.Hostname == request.Hostname || s.IPAddress == request.IPAddress);

            var existingScraper = await _scraperRepository.FirstOrDefaultAsync(spec);

            if (existingScraper is not null)
            {
                existingScraper.IsActive = true;
                existingScraper.RegisteredAt = _dateTimeProvider.UtcNow;
                existingScraper.ScraperName = request.ScraperName;

                await _scraperRepository.UpdateAsync(existingScraper);
                return Result.Success(existingScraper.Id);
            }

            var newScraper = new Scraper
            {
                Id = _idProvider.GenerateId<Scraper>(),
                Hostname = request.Hostname,
                IPAddress = request.IPAddress,
                ScraperName = request.ScraperName,
                RegisteredAt = _dateTimeProvider.UtcNow,
                IsActive = true,
                TasksHandled = 0
            };

            await _scraperRepository.AddAsync(newScraper);
            return Result.Success(newScraper.Id);
        }
    }
}
