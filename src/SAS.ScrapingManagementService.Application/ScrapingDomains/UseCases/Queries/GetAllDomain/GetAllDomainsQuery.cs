using Ardalis.Result;
using MediatR;
using SAS.ScrapingManagementService.Application.ScrapingDomains.Common;

namespace SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Queries.GetAllDomain
{
    // Query to get all DataSources paginated
    public record GetAllDomainsQuery(int? PageNumber, int? PageSize) : IRequest<Result<IEnumerable<ScrapingDomainDto>>>;
}
