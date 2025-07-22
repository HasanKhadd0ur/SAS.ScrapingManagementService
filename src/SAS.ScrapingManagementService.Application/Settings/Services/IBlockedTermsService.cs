using Ardalis.Result;
using SAS.ScrapingManagementService.Application.Settings.Common;

namespace SAS.ScrapingManagementService.Application.Settings.Services
{
    public interface IBlockedTermsService
    {
        Task<Result<List<BlockedTermDto>>> GetAllTermsAsync();
        Task<Result<BlockedTermDto>> GetTermByIdAsync(Guid id);
        Task<Result<BlockedTermDto>> CreateTermAsync(CreateBlockedTermDto dto);
        Task<Result<BlockedTermDto>> UpdateTermAsync(UpdateBlockedTermDto dto);
        Task<Result> DeleteTermAsync(Guid id);
    }
    public class CreateBlockedTermDto
    {
        public string Term { get; set; } = default!;
    }

    public class UpdateBlockedTermDto
    {
        public Guid Id { get; set; }
        public string Term { get; set; } = default!;
    }


}
