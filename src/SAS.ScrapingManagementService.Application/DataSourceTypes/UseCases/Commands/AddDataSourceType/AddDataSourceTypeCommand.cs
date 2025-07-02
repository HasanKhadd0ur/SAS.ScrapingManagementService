using Ardalis.Result;
using MediatR;

namespace SAS.ScrapingManagementService.Application.DataSourceTypes.UseCases.Commands.AddDataSourceType
{
    public record AddDataSourceTypeCommand(string Name) : IRequest<Result<Guid>>;
}
