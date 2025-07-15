using SAS.SharedKernel.DomainErrors;

namespace SAS.ScrapingManagementService.Domain.Settings.DomainErrors;

public static class PipelineErrors
{
    public static DomainError NotFound(Guid id) =>
       new("Pipeline.NotFound", $"Pipeline with ID '{id}' was not found.");
    public static DomainError StageNotFound(Guid stageId) =>
     new("Pipeline.StageNotFound", $"Stage with ID '{stageId}' was not found in the pipeline.");

}
