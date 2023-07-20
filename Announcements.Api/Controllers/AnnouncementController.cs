using Announcements.Test.Application.DTO;
using Announcements.Test.Application.Features.Announcements.Commands;
using Announcements.Test.Application.Features.Announcements.Queries;
using Announcements.WebApi.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Announcements.WebApi.Controllers
{
    [ApiController]
    [Route("~/api/announcements")]
    public class AnnouncementController : ApiBaseController
    {
        private readonly IMediator _mediator;
        private readonly AnnouncementOptions _announcementOptions;

        public AnnouncementController(IMediator mediator, IOptions<AnnouncementOptions> announcementOptions)
        {
            _mediator = mediator;

            _announcementOptions = announcementOptions.Value;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiOkResponse<List<AnnouncementDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAnnouncementsAsync([FromQuery] FilterAnnouncementsDto? filter,
            [FromQuery] PaginationDto? pagination, [FromQuery] SortAnnouncementsDto? sort, string? searchString,
            bool includeImage = false, CancellationToken cancellationToken = default)
        {

            var query = new GetAnnouncementsQuery
            {
                SearchString = searchString,
                Pagination = pagination,
                Filter = filter
            };

            var result = await _mediator.Send(query, cancellationToken);
            
            return ApiOk(result.Data);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(ApiOkResponse<AnnouncementDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAnnouncementAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAnnouncementQuery
            {
                Id = id
            });

            return ApiOk(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiOk201Response<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAnnouncementAsync([FromBody] CreateAnnouncementCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return ApiOk201(result.Data);
        }

        [HttpPatch]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAnnouncementAsync(Guid id, [FromBody] UpdateAnnouncementCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveAnnouncementAsync(Guid id, [FromBody] RemoveAnnouncementCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
