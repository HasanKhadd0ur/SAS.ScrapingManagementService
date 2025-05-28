namespace SAS.ScrapingManagementService.Application.DataSources.Common
{
    public class DataSourceDto
    {
        public string Name { get; set; }
        public string Target { get; set; }
        public int Limit { get; set; } = 1;
    }

}