using MediatR;
using SAS.ScrapingManagementService.Application.DataSources.Common;

namespace SAS.ScrapingManagementService.Application.DataSources.UseCases.Queries
{
    // Query to get a single DataSource by Id
    public record GetDataSourceByIdQuery(Guid Id) : IRequest<Ardalis.Result.Result<DataSourceDto>>;
}
