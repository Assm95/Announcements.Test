using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Announcements.Test.WebApi.Responses
{
    public class ApiBadRequestResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; }

        public ApiBadRequestResponse(ModelStateDictionary modelState)
            : base(StatusCodes.Status400BadRequest)
        {
            if (modelState.IsValid)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(modelState));
            }

            Errors = modelState
                .SelectMany(x => x.Value?.Errors.Select(y => x.Key + ": " + y.ErrorMessage) ??
                                 Array.Empty<string>())
                .ToArray();
        }

        public ApiBadRequestResponse(params string[] errors) : base(StatusCodes.Status400BadRequest)
        {
            Errors = errors;
        }
    }
}
