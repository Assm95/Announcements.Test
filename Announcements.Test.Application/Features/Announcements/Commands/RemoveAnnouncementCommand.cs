using Announcements.Test.Application.Common.Exceptions;
using Announcements.Test.Application.Interfaces.Repositories;
using Announcements.Test.Domain.Entities;
using Announcements.Test.Shared;
using MediatR;
using ApplicationException = Announcements.Test.Application.Common.Exceptions.ApplicationException;

namespace Announcements.Test.Application.Features.Announcements.Commands
{
    public class RemoveAnnouncementCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }

    internal class RemoveAnnouncementCommandHandler : IRequestHandler<RemoveAnnouncementCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _announcementsUnitOfWork;
        private readonly IUnitOfWork _usersUnitOfWork;

        public RemoveAnnouncementCommandHandler(IUnitOfWork announcementsUnitOfWork, IUnitOfWork usersUnitOfWork)
        {
            _announcementsUnitOfWork = announcementsUnitOfWork;
            _usersUnitOfWork = usersUnitOfWork;
        }

        public async Task<Result<Guid>> Handle(RemoveAnnouncementCommand request, CancellationToken cancellationToken)
        {
            User? user = await _usersUnitOfWork.Repository<User>().GetByIdAsync(request.UserId, cancellationToken);

            if (user == null)
                throw new NotFoundException("User not found");

            Announcement? announcement = await _announcementsUnitOfWork.Repository<Announcement>().GetByIdAsync(request.Id, cancellationToken);

            if (announcement == null)
                throw new NotFoundException("Announcement not found");

            if (user.IsAdmin || user.Id == announcement.Id)
            {
                await _announcementsUnitOfWork.Repository<Announcement>().DeleteAsync(announcement, cancellationToken);
                await _announcementsUnitOfWork.SaveAsync(cancellationToken);
            }
            else
                throw new ApplicationException("To update or remove announcements can only admin or owner.");

            return await Result<Guid>.SuccessAsync(request.Id, "Announcement was removed");
        }
    }
}

