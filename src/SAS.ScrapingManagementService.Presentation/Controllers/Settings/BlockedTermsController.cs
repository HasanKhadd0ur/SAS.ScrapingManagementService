using Microsoft.AspNetCore.Mvc;
using SAS.ScrapingManagementService.Application.Settings.Common;
using SAS.ScrapingManagementService.Application.Settings.Services;
using SAS.ScrapingManagementService.Presentation.Controllers.ApiBase;

namespace SAS.ScrapingManagementService.Presentation.Controllers.Settings
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlockedTermsController : APIController
    {
        private readonly IBlockedTermsService _blockedTermsService;

        public BlockedTermsController(IBlockedTermsService blockedTermsService)
        {
            _blockedTermsService = blockedTermsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _blockedTermsService.GetAllTermsAsync();
            return HandleResult(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _blockedTermsService.GetTermByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBlockedTermDto dto)
        {
            var result = await _blockedTermsService.CreateTermAsync(dto);
            return HandleResult(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBlockedTermDto dto)
        {
            var result = await _blockedTermsService.UpdateTermAsync(dto);
            return HandleResult(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _blockedTermsService.DeleteTermAsync(id);
            return HandleResult(result);
        }
    }
}
