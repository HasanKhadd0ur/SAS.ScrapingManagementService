using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.ConnectScraper;
using SAS.ScrapingManagementService.Presentation.Controllers.ApiBase;

namespace SAS.ScrapingManagementService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScrapersController : APIController
    {
        private readonly IMediator _mediator;

        public ScrapersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("connect")]
        public async Task<IActionResult> Connect([FromBody] ConnectScraperCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
    }
}