using Announcements.EF.Models;
using Announcements.EF.Services;
using Announcements.WebApi.Models;
using Announcements.WebApi.Queries;
using Announcements.WebApi.Requests;
using Announcements.WebApi.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using File = Announcements.EF.Models.File;

namespace Announcements.WebApi.Controllers
{
    [ApiController]
    [Route("~/api/announcements")]
    public class AnnouncementController : ApiBaseController
    {
        private readonly IAnnouncementService _announcementService;
        private readonly IUserService _userService;
        private readonly AnnouncementOptions _announcementOptions;

        public AnnouncementController(IAnnouncementService announcementService,
            IUserService userService, IOptions<AnnouncementOptions> announcementOptions)
        {
            _announcementService = announcementService;
            _userService = userService;
            _announcementOptions = announcementOptions.Value;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiOkResponse<ListDto<AnnouncementDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAnnouncementsAsync([FromQuery] GetAnnouncementsFilteringQuery? announcementsQuery,
            [FromQuery] PaginationQuery? paginationQuery, [FromQuery] GetAnnouncementsSortingQuery? sortingQuery, string? searchString,
            bool includeImage = false, CancellationToken cancellationToken = default)
        {
            var announcementsFilter = announcementsQuery.ToFilter();
            var paginationFilter = paginationQuery.ToFilter();
            var sortingFilter = sortingQuery.ToFilter();

            var result = await _announcementService.GetAllAsync(paginationFilter, announcementsFilter, searchString, sortingFilter, cancellationToken);

            long totalCount = result.TotalCount;
            AnnouncementDto[] announcements = result.Items.Select(x => x.ToDto(includeImage)).ToArray();

            ListDto<AnnouncementDto> announcementsList = new ListDto<AnnouncementDto>(announcements, totalCount);

            return ApiOk(announcementsList);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(ApiOkResponse<AnnouncementDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAnnouncementAsync(Guid id, CancellationToken cancellationToken)
        {
            Announcement announcement = await _announcementService.GetAsync(id, cancellationToken);
            AnnouncementDto announcementDto = announcement.ToDto();

            return ApiOk(announcementDto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiOk201Response<AnnouncementDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAnnouncementAsync([FromBody] CreateAnnouncementRequest request,
            CancellationToken cancellationToken)
        {
            User user = await _userService.Get(request.UserId, cancellationToken);

            if (user.Announcements.Count >= _announcementOptions.UserLimit)
                return ApiBadRequest("The maximum number of announcements has been reached.");

            File file =  request.Image.ToDomain();

            Announcement announcement = await _announcementService.CreateAsync(user, request.Number, request.Text, file,
                request.Rating, request.ExpirationDate.ToDateTime(TimeOnly.MinValue), cancellationToken);

            AnnouncementDto announcementDto = announcement.ToDto();

            return ApiOk201(announcementDto);
        }

        [HttpPatch]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAnnouncementAsync(Guid id, [FromBody] UpdateAnnouncementRequest request,
            CancellationToken cancellationToken)
        {
            User user = await _userService.Get(request.UserId, cancellationToken);
            File file = request.Image.ToDomain();

            await _announcementService.UpdateAsync(id, user, request.Number, request.Text, file, request.Rating,
                request.ExpirationDate.ToDateTime(TimeOnly.MinValue), cancellationToken);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveAnnouncementAsync(Guid id, [FromBody] RemoveAnnouncementRequest request,
            CancellationToken cancellationToken)
        {
            User user = await _userService.Get(request.UserId, cancellationToken);

            await _announcementService.RemoveAsync(id, user, cancellationToken);

            return NoContent();
        }
    }
}
