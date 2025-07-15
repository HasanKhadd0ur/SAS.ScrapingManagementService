using SAS.SharedKernel.Entities;

namespace SAS.ScrapingManagementService.Domain.Settings.Entities
{
    public class PipelineConfig : BaseEntity<Guid>
    {
        public string PipelineKey { get; set; }  // e.g. "preprocessing", "publishing"

        public string Version { get; set; }  // config version

        // list of stages in this pipeline
        public ICollection<PipelineStage> Stages { get; set; } = new List<PipelineStage>();
    }
}
