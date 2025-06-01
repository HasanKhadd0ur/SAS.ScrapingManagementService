using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.AssignScrapingExecutor;
using SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.CompleteScrapingTask;
using SAS.ScrapingManagementService.Presentation.Controllers.ApiBase;

namespace SAS.ScrapingManagementService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScrapingTasksController : APIController
    {
        private readonly IMediator _mediator;

        public ScrapingTasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id:guid}/complete")]
        public async Task<IActionResult> CompleteScrapingTask(Guid id)
        {
            var result = await _mediator.Send(new CompleteScrapingTaskCommand(id));
            return HandleResult(result);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignExecutor([FromBody] AssignScrapingExecutorCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
    }
}
