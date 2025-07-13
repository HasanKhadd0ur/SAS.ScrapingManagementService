using Ardalis.Result;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.DeleteDataSource
{
    public sealed record DeleteDataSourceCommand(Guid Id) : ICommand<Result>;
}
