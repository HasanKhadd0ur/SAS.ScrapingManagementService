namespace SAS.ScrapingManagementService.Application.Common
{
    public class BaseDTO<T>
    {
        public T Id { get; set; }
    }
    public class PaginatedDTO<T> : BaseDTO<T>
    {
        public IEnumerable<T> Items { get; init; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        //public int TotalCount { get; init; }
    }
}
