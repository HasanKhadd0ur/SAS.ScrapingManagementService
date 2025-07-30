using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.AssignScrapingExecutor;
using SAS.ScrapingManagementService.Application.ScrapingTasks.UseCases.Commands.CompleteScrapingTask;
using SAS.ScrapingManagementService.Application.ScrapingTasks.UseCases.Queries.GetAllScrapingTasks;
using SAS.ScrapingManagementService.Application.ScrapingTasks.UseCases.Queries.GetScrapingTaskById;
using SAS.ScrapingManagementService.Presentation.Controllers.ApiBase;

namespace SAS.ScrapingManagementService.Presentation.Controllers.ScrapingTasks
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
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var result = await _mediator.Send(new GetAllScrapingTasksQuery(pageNumber, pageSize));
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetScrapingTaskByIdQuery(id));
            return HandleResult(result);
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
