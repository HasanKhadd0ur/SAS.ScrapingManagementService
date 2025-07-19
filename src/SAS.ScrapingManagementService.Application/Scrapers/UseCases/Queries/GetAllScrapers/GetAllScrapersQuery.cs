using Ardalis.Result;
using SAS.ScrapingManagementService.Application.Scrapers.Common;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.ScrapingManagementService.Application.Scrapers.UseCases.Queries.GetAllScrapers
{
    public record GetAllScrapersQuery(int? PageNumber, int? PageSize) : IQuery<Result<List<ScraperDto>>>;
}
