namespace SAS.ScrapingManagementService.Application.Contracts.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipient, string subject, string body);
        Task SendBulkEmailsAsync(IEnumerable<string> recipient, string subject, string body);

    }
}
