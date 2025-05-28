namespace SAS.ScrapingManagementService.Application.Contracts.Users
{
    public interface IUserService
    {
        Task<UserDto> GetCurrentUserAsync();
        Task<List<Guid>> GetUserIdsInterestedInRegionAsync(string regionName);
        Task<List<Guid>> GetUserIdsInterestedInTopicAsync(string topicName);
        Task<List<string>> GetUserEmailsByIdsAsync(List<Guid> userIds);
    }

}
