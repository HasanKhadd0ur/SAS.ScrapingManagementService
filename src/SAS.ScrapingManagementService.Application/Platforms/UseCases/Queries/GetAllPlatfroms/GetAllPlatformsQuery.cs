using Ardalis.Result;
using MediatR;
using SAS.ScrapingManagementService.Application.Platforms.Common;

namespace SAS.ScrapingManagementService.Application.Platforms.UseCases.Queries.GetAllPlatfroms
{
    // Query to get all Platforms paginated
   public record GetAllPlatformsQuery(int? PageNumber, int? PageSize) : IRequest<Result<IEnumerable<PlatformDto>>>;
}
