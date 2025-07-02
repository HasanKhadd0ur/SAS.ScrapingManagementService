using Ardalis.Result;
using MediatR;
using SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.DeleteDataSource;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.DomainErrors;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.Entities;
using SAS.ScrapingManagementService.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Commands.DeleteScrapingDomain
{
    public sealed class DeleteScrapingDomainCommandHandler : IRequestHandler<DeleteScrapingDomainCommand, Result>
        {
            private readonly IRepository<ScrapingDomain, Guid> _repo;

            public DeleteScrapingDomainCommandHandler(IRepository<ScrapingDomain, Guid> scrapingDomainRepo)
            {
            _repo = scrapingDomainRepo;
            }

            public async Task<Result> Handle(DeleteScrapingDomainCommand request, CancellationToken cancellationToken)
            {
                var domain = await _repo.GetByIdAsync(request.Id);
                if (domain is null)
                    return Result.Invalid(ScrapingDomainErrors.UnExistDomain);

                await _repo.DeleteAsync(domain);

                return Result.Success();
            }
        }
    }