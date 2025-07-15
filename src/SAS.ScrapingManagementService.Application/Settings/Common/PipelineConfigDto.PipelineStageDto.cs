using SAS.ScrapingManagementService.Application.Common;

namespace SAS.ScrapingManagementService.Application.Settings.Common
    {
    public partial class PipelineConfigDto
    {
        public class PipelineStageDto : BaseDTO<Guid>
        {
            public string StageName { get; set; } = default!;
            public int Order { get; set; }

            public string ParametersJson { get; set; } = string.Empty;
        }
    }
}
