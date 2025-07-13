using System.Security.Claims;

namespace SAS.ScrapingManagementService.Application.Contracts.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Region { get; set; }
        public List<string> Roles { get; set; }
        public IEnumerable<Claim> Claims { get; set; } = new List<Claim>();
    }
    
}
