using Announcements.Test.Application.Common.Exceptions;
using Announcements.Test.Application.DTO;
using Announcements.Test.Application.Interfaces;
using Announcements.Test.Application.Interfaces.Repositories;
using Announcements.Test.Domain.Entities;
using Announcements.Test.Shared;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Announcements.Test.Application.Features.Announcements.Queries
{
    public class GetAnnouncementQuery : IRequest<Result<AnnouncementDto>>
    {
        public Guid Id { get; set; }
    }

    internal class GetAnnouncementQueryHandler : IRequestHandler<GetAnnouncementQuery, Result<AnnouncementDto>>
    {
        private readonly IUnitOfWork _announcementsUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;

        public GetAnnouncementQueryHandler(IUnitOfWork announcementsUnitOfWork, IMapper mapper, IFileStorage fileStorage)
        {
            _announcementsUnitOfWork = announcementsUnitOfWork;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        public async Task<Result<AnnouncementDto>> Handle(GetAnnouncementQuery request, CancellationToken cancellationToken)
        {
            var query = _announcementsUnitOfWork.Repository<Announcement>().Entities;
            query = query.Include(x => x.User);

            Announcement ? announcement = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (announcement == null)
                throw new NotFoundException("Announcement not found");

            AnnouncementDto announcementDto = _mapper.Map<AnnouncementDto>(announcement);

            return await Task.FromResult(new Result<AnnouncementDto>(announcementDto));
        }
    }
}
