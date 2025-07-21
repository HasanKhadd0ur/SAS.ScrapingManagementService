using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.ConnectScraper;
using SAS.ScrapingManagementService.Application.Scrapers.UseCases.Queries.GetAllScrapers;
using SAS.ScrapingManagementService.Application.Settings.Services;
using SAS.ScrapingManagementService.Presentation.Controllers.ApiBase;

namespace SAS.ScrapingManagementService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScrapersController : APIController
    {
        private readonly IMediator _mediator;
        private readonly IBlockedTermsService _blockedTermService;


        public ScrapersController(IMediator mediator, IBlockedTermsService blockedTermService)
        {
            _mediator = mediator;
            _blockedTermService = blockedTermService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var query = new GetAllScrapersQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }

        [HttpPost("connect")]
        public async Task<IActionResult> Connect([FromBody] ConnectScraperCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpGet("BlockedTerems")]
        public async Task<IActionResult> GetBlockedTerms()
        {
            var result = await _blockedTermService.GetAllTermsAsync();
            return HandleResult(result);

        }
    }
}
