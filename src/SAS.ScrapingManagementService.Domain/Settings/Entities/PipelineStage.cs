using SAS.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Domain.Settings.Entities
{
    public class PipelineStage : BaseEntity<Guid>
    {
        public string StageName { get; set; }  // e.g. "NormalizeTextStage"

        public int Order { get; set; }  // Execution order of the stage

        public Guid PipelineConfigId { get; set; }
        public PipelineConfig PipelineConfig { get; set; }

        public string ParametersJson { get; set; }
    }
}
