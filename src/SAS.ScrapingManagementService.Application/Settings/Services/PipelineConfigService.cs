using Ardalis.Result;
using AutoMapper;
using SAS.ScrapingManagementService.Application.Contracts.Providers;
using SAS.ScrapingManagementService.Application.Settings.Common;
using SAS.ScrapingManagementService.Domain.Settings.DomainErrors;
using SAS.ScrapingManagementService.Domain.Settings.Entities;
using SAS.ScrapingManagementService.Domain.Settings.Repositories;
using SAS.SharedKernel.DomainErrors;
using SAS.SharedKernel.Specification;
using static SAS.ScrapingManagementService.Application.Settings.Common.PipelineConfigDto;

namespace SAS.ScrapingManagementService.Application.Settings.Services
{
    public class PipelineConfigService : IPipelineConfigService
    {
        private readonly IPipelineRepository _repository;
        private readonly IMapper _mapper;
        private readonly IIdProvider _idProvider;

        public PipelineConfigService(
            IPipelineRepository repository,
            IMapper mapper,
            IIdProvider idProvider)
        {
            _repository = repository;
            _mapper = mapper;
            _idProvider = idProvider;
        }

        public async Task<Result<List<PipelineConfigDto>>> GetAllAsync()
        {

            var spec = new BaseSpecification<PipelineConfig>();
            spec.AddInclude(e => e.Stages);

            var configs = await _repository.ListAsync(spec);
            var mapped = _mapper.Map<List<PipelineConfigDto>>(configs);
            return Result.Success(mapped);
        }

        public async Task<Result<PipelineConfigDto>> GetByIdAsync(Guid id)
        {
            var spec = new BaseSpecification<PipelineConfig>();
            spec.AddInclude(e => e.Stages);
            
            var config = await _repository.GetByIdAsync(id,spec);

            if (config is null)
                return Result.Invalid(PipelineErrors.NotFound(id));
            
            
            var dto = _mapper.Map<PipelineConfigDto>(config);
            return Result.Success(dto);
        }

        public async Task<Result<Guid>> CreateAsync(PipelineConfigDto dto)
        {
            var entity = _mapper.Map<PipelineConfig>(dto);
            entity.Id = _idProvider.GenerateId<PipelineConfig>();
            await _repository.AddAsync(entity);
            return Result.Success(entity.Id);
        }

        public async Task<Result> UpdateAsync(PipelineConfigDto dto)
        {
            var spec = new BaseSpecification<PipelineConfig>();
            spec.AddInclude(e => e.Stages);

            var existing = await _repository.GetByIdAsync(dto.Id,spec);
            if (existing is null)
                return Result.Invalid(PipelineErrors.NotFound(dto.Id));

            existing.PipelineKey = dto.PipelineKey;
            existing.Version = dto.Version;

            existing.Stages.Clear();
            var newStages = _mapper.Map<List<PipelineStage>>(dto.Stages);
            foreach (var stage in newStages)
                existing.Stages.Add(stage);

            await _repository.UpdateAsync(existing);
            return Result.Success();
        }

        public async Task<Result> AddStageAsync(Guid pipelineId, PipelineStageDto stageDto)
        {

            var spec = new BaseSpecification<PipelineConfig>();
            spec.AddInclude(e => e.Stages);

            var config = await _repository.GetByIdAsync(pipelineId, spec);
            if (config is null)
                return Result.Invalid(PipelineErrors.NotFound(pipelineId));

            var newStage = _mapper.Map<PipelineStage>(stageDto);
            newStage.Id = _idProvider.GenerateId<PipelineStage>();
            config.Stages.Add(newStage);

            await _repository.UpdateAsync(config);
            return Result.Success();
        }

        public async Task<Result> RemoveStageAsync(Guid pipelineId, Guid stageId)
        {

            var spec = new BaseSpecification<PipelineConfig>();
            spec.AddInclude(e => e.Stages);

            var config = await _repository.GetByIdAsync(pipelineId, spec);
            if (config is null)
                return Result.Invalid(PipelineErrors.NotFound(pipelineId));

            var stage = config.Stages.FirstOrDefault(s => s.Id == stageId);
            if (stage is null)
                return Result.Invalid(PipelineErrors.StageNotFound(stageId));

            config.Stages.Remove(stage);
            await _repository.UpdateAsync(config);
            return Result.Success();
        }
    }

}
