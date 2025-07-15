using Ardalis.Result;
using SAS.ScrapingManagementService.Application.Settings.Common;
using static SAS.ScrapingManagementService.Application.Settings.Common.PipelineConfigDto;

namespace SAS.ScrapingManagementService.Application.Settings.Services
{
    public interface IPipelineConfigService
    {
        Task<Result<List<PipelineConfigDto>>> GetAllAsync();
        Task<Result<PipelineConfigDto>> GetByIdAsync(Guid id);

        Task<Result<Guid>> CreateAsync(PipelineConfigDto dto);
        Task<Result> UpdateAsync(PipelineConfigDto dto);
        Task<Result> AddStageAsync(Guid pipelineId, PipelineStageDto stageDto);
        Task<Result> RemoveStageAsync(Guid pipelineId, Guid stageId);


    }
}
