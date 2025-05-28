namespace SAS.ScrapingManagementService.Application.Common
{
    public class BaseDTO<T>
    {
        public T Id { get; set; }
    }
    public class PaginatedDTO<T> : BaseDTO<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
