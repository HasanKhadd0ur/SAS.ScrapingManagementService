using Ardalis.Result;
using MediatR;
using SAS.ScrapingManagementService.Application.Scrapers.Common;

namespace SAS.ScrapingManagementService.Application.Scrapers.UseCases.Queries.GetAllScrapers
{
    public record GetAllScrapersQuery(int? PageNumber, int? PageSize) : IRequest<Result<IEnumerable<ScraperDto>>>;
}
