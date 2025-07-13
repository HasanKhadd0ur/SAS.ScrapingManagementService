using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.ScrapingManagementService.Application.DataSources.UseCases.Queries;
using SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Commands.CreateScrapingDomain;
using SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Commands.DeleteScrapingDomain;
using SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Commands.UpdateScrapingDomain;
using SAS.ScrapingManagementService.Application.ScrapingDomains.UseCases.Queries.GetAllDomain;
using SAS.ScrapingManagementService.Presentation.Controllers.ApiBase;

namespace SAS.ScrapingManagementService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScrapingDomainsController : APIController
    {
        private readonly IMediator _mediator;

        public ScrapingDomainsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateScrapingDomainCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateScrapingDomainCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteScrapingDomainCommand(id));
            return HandleResult(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetDomainByIdQuery(id));
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var result = await _mediator.Send(new GetAllDomainsQuery(pageNumber, pageSize));
            return HandleResult(result);
        }
    }
}
