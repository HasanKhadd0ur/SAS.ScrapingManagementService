using Ardalis.Result;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.AddDataSource
{
    public sealed record AddDataSourceCommand(
    Guid DomainId,
    Guid PlatformId,
    Guid DataSourceTypeId,
    string Name,
    string Target) : ICommand<Result<Guid>>;

}
