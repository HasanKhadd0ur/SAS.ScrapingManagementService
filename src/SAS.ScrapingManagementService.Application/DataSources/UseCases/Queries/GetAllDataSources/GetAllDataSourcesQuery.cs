using Ardalis.Result;
using MediatR;
using SAS.ScrapingManagementService.Application.Common;
using SAS.ScrapingManagementService.Application.DataSources.Common;

namespace SAS.ScrapingManagementService.Application.DataSources.UseCases.Queries.GetAllDataSources
{
    // Query to get all DataSources paginated
    public record GetAllDataSourcesQuery(int? PageNumber, int? PageSize) : IRequest<Result<IEnumerable<DataSourceDto>>>;
}
