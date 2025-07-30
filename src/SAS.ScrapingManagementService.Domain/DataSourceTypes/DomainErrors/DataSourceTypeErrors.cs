using SAS.SharedKernel.DomainErrors;

namespace SAS.ScrapingManagementService.Domain.DataSourceTypes.DomainErrors;

public static class DataSourceTypeErrors
{
    public static readonly DomainError UnExistType =
         new("DataSourceTypeError.UnExistType", "Data Source Type un exist.");
    public static DomainError AlreadyExists(string name) =>
            new("DataSourceTypeError.AlreadyExists", $"A Data Source Type  with the name '{name}' already exists.");
}
