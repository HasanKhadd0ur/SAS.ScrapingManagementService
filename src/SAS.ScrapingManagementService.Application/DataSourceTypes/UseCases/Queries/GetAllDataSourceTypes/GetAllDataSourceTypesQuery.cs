using Ardalis.Result;
using MediatR;
using SAS.ScrapingManagementService.Application.DataSourceTypes.Common;

namespace SAS.ScrapingManagementService.Application.DataSourceTypes.UseCases.Queries.GetAllDataSourceTypes
{
    public record GetAllDataSourceTypesQuery(int? PageNumber, int? PageSize) : IRequest<Result<IEnumerable<DataSourceTypeDto>>>;
}
