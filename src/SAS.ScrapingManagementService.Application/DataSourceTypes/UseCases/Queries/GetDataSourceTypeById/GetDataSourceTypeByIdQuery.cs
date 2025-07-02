using Ardalis.Result;
using MediatR;
using SAS.ScrapingManagementService.Application.DataSourceTypes.Common;

namespace SAS.ScrapingManagementService.Application.DataSourceTypes.UseCases.Queries.GetDataSourceTypeById
{
    public record GetDataSourceTypeByIdQuery(Guid Id) : IRequest<Result<DataSourceTypeDto>>;
}