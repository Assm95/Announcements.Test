using Announcements.EF.Exceptions;
using Announcements.WebApi.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Announcements.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ErrorController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("/error")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            var exception = context.Error;

            ApiResponse? result;

            if (exception is DomainNotFoundException)
            {
                result = new ApiResponse(StatusCodes.Status404NotFound, exception.Message);
            }
            else if (exception is DomainAccessDeniedException)
            {
                result = new ApiResponse(StatusCodes.Status403Forbidden, exception.Message);
            }
            else if (exception is DomainException)
            {
                result = new ApiBadRequestResponse(exception.Message);
            }
            else
            {
                string? details = _webHostEnvironment.IsDevelopment() ? context.Error.StackTrace : null;

                result = new ApiInternalServerErrorResponse(exception.Message, details);
            }

            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }

        [Route("/error/{code}")]
        public IActionResult HandleStatusCode(int code)
        {
            return new ObjectResult(new ApiResponse(code))
            {
                StatusCode = code
            };
        }
    }
}
