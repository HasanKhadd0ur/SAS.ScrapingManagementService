using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.AddDataSource;
using SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.DeleteDataSource;
using SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.UpdateDataSource;
using SAS.ScrapingManagementService.Application.DataSources.UseCases.Queries;
using SAS.ScrapingManagementService.Application.DataSources.UseCases.Queries.GetAllDataSources;
using SAS.ScrapingManagementService.Presentation.Controllers.ApiBase;

namespace SAS.ScrapingManagementService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataSourcesController : APIController
    {
        private readonly IMediator _mediator;

        public DataSourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddDataSourceCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDataSourceCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteDataSourceCommand(id));
            return HandleResult(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetDataSourceByIdQuery(id));
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? pageNumber , [FromQuery] int? pageSize)
        {
            var result = await _mediator.Send(new GetAllDataSourcesQuery(pageNumber, pageSize));
            return HandleResult(result);
        }
    }

}
