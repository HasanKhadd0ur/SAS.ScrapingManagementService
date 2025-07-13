using Ardalis.Result;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Commands.DeleteScrapingDomain
{
    public sealed record DeleteScrapingDomainCommand(Guid Id) : ICommand<Result>;
}
