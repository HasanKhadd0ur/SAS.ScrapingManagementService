using SAS.ScrapingManagementService.SharedKernel.DomainErrors;

namespace SAS.ScrapingManagementService.Domain.DataSources.DomainErrors;

public static class DataSourceErrors
{
    public static readonly DomainError UnExistDataSource=
         new("DataSourceError.UnExistDataSource", "Data Source un exist.");

}