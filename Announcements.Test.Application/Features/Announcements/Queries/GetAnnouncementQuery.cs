using Announcements.Test.Application.Common.Exceptions;
using Announcements.Test.Application.DTO;
using Announcements.Test.Application.Interfaces.Repositories;
using Announcements.Test.Domain.Entities;
using Announcements.Test.Shared;
using MediatR;

namespace Announcements.Test.Application.Features.Announcements.Queries
{
    public class GetAnnouncementQuery : IRequest<Result<AnnouncementDto>>
    {
        public Guid Id { get; set; }
    }

    internal class GetAnnouncementQueryHandler : IRequestHandler<GetAnnouncementQuery, Result<AnnouncementDto>>
    {
        private readonly IUnitOfWork _announcementsUnitOfWork;

        public GetAnnouncementQueryHandler(IUnitOfWork announcementsUnitOfWork)
        {
            _announcementsUnitOfWork = announcementsUnitOfWork;
        }

        public async Task<Result<AnnouncementDto>> Handle(GetAnnouncementQuery request, CancellationToken cancellationToken)
        {
            Announcement? announcement = await _announcementsUnitOfWork.Repository<Announcement>()
                .GetByIdAsync(request.Id, cancellationToken);

            if (announcement == null)
                throw new NotFoundException("Announcement not found");

            //TODO маппинг в DTO

            AnnouncementDto announcementDto = new AnnouncementDto();

            return await Result<AnnouncementDto>.SuccessAsync(announcementDto);
        }
    }
}
