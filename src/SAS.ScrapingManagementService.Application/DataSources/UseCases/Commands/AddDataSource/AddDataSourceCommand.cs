using Ardalis.Result;
using SAS.ScrapingManagementService.SharedKernel.CQRS.Commands;

namespace SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.AddDataSource
{
    public sealed record AddDataSourceCommand(
    Guid DomainId,
    Guid PlatformId,
    string Name,
    string Traget) : ICommand<Result<Guid>>;

}