using Microsoft.AspNetCore.Mvc;
using SAS.ScrapingManagementService.Application.Settings.Common;
using SAS.ScrapingManagementService.Application.Settings.Services;
using SAS.ScrapingManagementService.Presentation.Controllers.ApiBase;

namespace SAS.ScrapingManagementService.Presentation.Controllers.Settings
{
    [ApiController]
    [Route("api/[controller]")]
    public class PipelineConfigsController : APIController
    {
        private readonly IPipelineConfigService _pipelineConfigService;

        public PipelineConfigsController(IPipelineConfigService pipelineConfigService)
        {
            _pipelineConfigService = pipelineConfigService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _pipelineConfigService.GetAllAsync();
            return HandleResult(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _pipelineConfigService.GetByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PipelineConfigDto dto)
        {
            var result = await _pipelineConfigService.CreateAsync(dto);
            return HandleResult(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PipelineConfigDto dto)
        {
            if (id != dto.Id)
                return BadRequest("PipelineConfig ID mismatch.");

            var result = await _pipelineConfigService.UpdateAsync(dto);
            return HandleResult(result);
        }
    }
}
