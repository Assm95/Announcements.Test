using Announcements.Test.Application.DTO;
using Announcements.Test.Application.Features.Announcements.Commands;
using Announcements.Test.Application.Features.Announcements.Queries;
using Announcements.Test.WebApi.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Announcements.Test.WebApi.Controllers
{
    [ApiController]
    [Route("~/api/announcements")]
    public class AnnouncementController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public AnnouncementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiPagedOkResponse<List<AnnouncementDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAnnouncementsAsync([FromQuery] FilterAnnouncementsDto? filter,
            [FromQuery] PaginationDto? pagination, [FromQuery] SortAnnouncementsDto? sort, string? searchString,
            bool includeImageData = false, CancellationToken cancellationToken = default)
        {

            var query = new GetAnnouncementsQuery
            {
                SearchString = searchString,
                Pagination = pagination,
                Filter = filter,
                Sort = sort,
                IncludeImageData = includeImageData
            };

            var result = await _mediator.Send(query, cancellationToken);
            var pagedData = result.Data;
            var response = new ApiPagedOkResponse<List<AnnouncementDto>>(pagedData.Data, pagedData.PageNumber,
                pagedData.PageSize, pagedData.TotalCount, pagedData.PagesCount);

            return Ok(response);
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
            }, cancellationToken);

            return ApiOk(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiOkResponse<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAnnouncementAsync([FromBody] CreateAnnouncementCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return ApiOk(result.Data);
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
