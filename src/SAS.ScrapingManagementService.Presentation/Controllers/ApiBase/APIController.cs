using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SAS.ScrapingManagementService.Presentation.Controllers.ApiBase
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return Problem(
                    detail: result.ValidationErrors.FirstOrDefault().ErrorMessage,
                    statusCode: StatusCodes.Status400BadRequest,
                    title: result.ValidationErrors.FirstOrDefault().ErrorCode
                    );
            }
        }

    }
}
