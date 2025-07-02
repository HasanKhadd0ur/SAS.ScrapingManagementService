using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.ScrapingManagementService.Application.DataSourceTypes.UseCases.Commands.AddDataSourceType;
using SAS.ScrapingManagementService.Application.DataSourceTypes.UseCases.Queries.GetAllDataSourceTypes;
using SAS.ScrapingManagementService.Application.DataSourceTypes.UseCases.Queries.GetDataSourceTypeById;
using SAS.ScrapingManagementService.Presentation.Controllers.ApiBase;

namespace SAS.ScrapingManagementService.Presentation.Controllers.DataSourceTypes
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataSourceTypesController : APIController
    {
        private readonly IMediator _mediator;

        public DataSourceTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddDataSourceTypeCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetDataSourceTypeByIdQuery(id));
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var result = await _mediator.Send(new GetAllDataSourceTypesQuery(pageNumber, pageSize));
            return HandleResult(result);
        }
    }
}
