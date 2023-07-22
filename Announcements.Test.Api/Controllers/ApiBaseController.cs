using Announcements.Test.WebApi.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Announcements.Test.WebApi.Controllers
{
    public class ApiBaseController : Controller
    {
        protected IActionResult ApiBadRequest(params string[] errors)
        {
            return BadRequest(new ApiBadRequestResponse(errors));
        }

        protected IActionResult ApiBadRequest(IEnumerable<string> errors)
        {
            return BadRequest(new ApiBadRequestResponse(errors.ToArray()));
        }

        protected IActionResult ApiNotFound(string? message = null)
        {
            return NotFound(new ApiResponse(StatusCodes.Status404NotFound, message));
        }
        
        protected IActionResult ApiOk<T>(T result)
        {
            return Ok(new ApiOkResponse<T>(result));
        }
    }
}
