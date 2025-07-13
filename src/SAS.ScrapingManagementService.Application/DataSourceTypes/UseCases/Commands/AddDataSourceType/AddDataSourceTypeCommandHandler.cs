using Ardalis.Result;
using MediatR;
using SAS.ScrapingManagementService.Application.Contracts.Providers;
using SAS.ScrapingManagementService.Domain.DataSources.DomainErrors;
using SAS.ScrapingManagementService.Domain.DataSources.Entities;
using SAS.ScrapingManagementService.Domain.DataSources.Repositories;
using SAS.SharedKernel.Repositories;

namespace SAS.ScrapingManagementService.Application.DataSourceTypes.UseCases.Commands.AddDataSourceType
{
    public sealed class AddDataSourceTypeCommandHandler : IRequestHandler<AddDataSourceTypeCommand, Result<Guid>>
    {
        private readonly IDataSourceTypeRepository _typeRepo;
        private readonly IIdProvider _idProvider;

        public AddDataSourceTypeCommandHandler(
            IDataSourceTypeRepository typeRepo,
            IIdProvider idProvider)
        {
            _typeRepo = typeRepo;
            _idProvider = idProvider;
        }

        public async Task<Result<Guid>> Handle(AddDataSourceTypeCommand request, CancellationToken cancellationToken)
        {
            // Optional: check for duplicate name
            var existing = await _typeRepo.GetByNameAsync(request.Name);
            if (existing is not null)
                return Result.Invalid(DataSourceTypeErrors.AlreadyExists(request.Name));

            DataSourceType type = new DataSourceType
            {
                Id = _idProvider.GenerateNewId(),
                Name = request.Name.Trim()
            };

            await _typeRepo.AddAsync(type);

            return Result.Success(type.Id);
        }
    }
}
