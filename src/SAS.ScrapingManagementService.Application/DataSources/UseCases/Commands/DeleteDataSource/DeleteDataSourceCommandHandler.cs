using Ardalis.Result;
using MediatR;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.ScrapingDomains.DomainErrors;
using SAS.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.DeleteDataSource
{
    public sealed class DeleteDataSourceCommandHandler : IRequestHandler<DeleteDataSourceCommand, Result>
        {
            private readonly IRepository<DataSource, Guid> _dataSourceRepo;

            public DeleteDataSourceCommandHandler(IRepository<DataSource, Guid> dataSourceRepo)
            {
                _dataSourceRepo = dataSourceRepo;
            }

            public async Task<Result> Handle(DeleteDataSourceCommand request, CancellationToken cancellationToken)
            {
                var dataSource = await _dataSourceRepo.GetByIdAsync(request.Id);
                if (dataSource is null)
                    return Result.Invalid(ScrapingDomainErrors.UnExistDomain);

                await _dataSourceRepo.DeleteAsync(dataSource);

                return Result.Success();
            }
        }
    }
