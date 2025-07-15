using SAS.ScrapingManagementService.Application.Common;

namespace SAS.ScrapingManagementService.Application.Settings.Common
    {
    public partial class PipelineConfigDto : BaseDTO<Guid>
    {
        public string PipelineKey { get; set; } = default!;
        public string Version { get; set; } = default!;

        public List<PipelineStageDto> Stages { get; set; } = new();
    }
}
