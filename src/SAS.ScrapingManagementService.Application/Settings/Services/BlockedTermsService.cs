using Ardalis.Result;
using AutoMapper;
using SAS.ScrapingManagementService.Application.Settings.Common;
using SAS.ScrapingManagementService.Domain.DataSources.DomainErrors;
using SAS.ScrapingManagementService.Domain.Settings.Entities;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Specification;

namespace SAS.ScrapingManagementService.Application.Settings.Services
{
    public class BlockedTermsService : IBlockedTermsService
    {
        private readonly IRepository<BlockedTerm, Guid> _repository;
        private readonly IMapper _mapper;

        public BlockedTermsService(IRepository<BlockedTerm, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<BlockedTermDto>>> GetAllTermsAsync()
        {
            var spec = new BaseSpecification<BlockedTerm>();
            var terms = await _repository.ListAsync(spec);
            var dtoList = _mapper.Map<List<BlockedTermDto>>(terms);
            return Result.Success(dtoList);
        }

        public async Task<Result<BlockedTermDto>> GetTermByIdAsync(Guid id)
        {
            var term = await _repository.GetByIdAsync(id);
            if (term is null)
                return Result.Invalid(DataSourceErrors.UnExistDataSource);

            return Result.Success(_mapper.Map<BlockedTermDto>(term));
        }

        public async Task<Result<BlockedTermDto>> CreateTermAsync(CreateBlockedTermDto dto)
        {
            var entity = new BlockedTerm { Id = Guid.NewGuid(), Term = dto.Term };
            await _repository.AddAsync(entity);
            return Result.Success(_mapper.Map<BlockedTermDto>(entity));
        }

        public async Task<Result<BlockedTermDto>> UpdateTermAsync(UpdateBlockedTermDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity is null)
                return Result.Invalid(DataSourceErrors.UnExistDataSource);

            entity.Term = dto.Term;
            await _repository.UpdateAsync(entity);
            return Result.Success(_mapper.Map<BlockedTermDto>(entity));
        }

        public async Task<Result> DeleteTermAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return Result.Invalid(DataSourceErrors.UnExistDataSource);

            await _repository.DeleteAsync(entity);
            return Result.Success();
        }
    }

}
