using MediatR;
using SAS.ScrapingManagementService.Application.DataSources.Common;
using SAS.ScrapingManagementService.Application.ScrapingDomains.Common;

namespace SAS.ScrapingManagementService.Application.DataSources.UseCases.Queries
{
    // Query to get a single DataSource by Id
    public record GetDomainByIdQuery(Guid Id) : IRequest<Ardalis.Result.Result<ScrapingDomainDto>>;
}
