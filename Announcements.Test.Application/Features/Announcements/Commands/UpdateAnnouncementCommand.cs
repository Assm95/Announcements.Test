using Announcements.Test.Application.Interfaces.Repositories;
using Announcements.Test.Application.Interfaces;
using Announcements.Test.Domain.Entities;
using Announcements.Test.Shared;
using MediatR;
using Announcements.Test.Application.Common.Exceptions;
using Announcements.Test.Application.DTO;
using File = Announcements.Test.Domain.Common.ValueObjects.File;

namespace Announcements.Test.Application.Features.Announcements.Commands
{
    public class UpdateAnnouncementCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime ExpirationDate { get; set; }

        public int Rating { get; set; }

        public Guid UserId { get; set; }

        public string FileName { get; set; } = string.Empty;

        public byte[] FileData { get; set; } = Array.Empty<byte>();
    }

    internal class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _announcementsUnitOfWork;
        private readonly IUnitOfWork _usersUnitOfWork;
        private readonly IFileStorage _fileStorage;

        public UpdateAnnouncementCommandHandler(IUnitOfWork announcementsUnitOfWork, IUnitOfWork usersUnitOfWork, IFileStorage fileStorage)
        {
            _announcementsUnitOfWork = announcementsUnitOfWork;
            _usersUnitOfWork = usersUnitOfWork;
            _fileStorage = fileStorage;
        }
        public async Task<Result<Guid>> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            return await ExceptionWrapper<Result<Guid>>.Catch(async () =>
            {
                User? user = await _usersUnitOfWork.Repository<User>().GetByIdAsync(request.UserId, cancellationToken);

                if (user == null)
                    throw new NotFoundException("User not found");

                Announcement? announcement = await _announcementsUnitOfWork.Repository<Announcement>()
                    .GetByIdAsync(request.Id, cancellationToken);

                if (announcement == null)
                    throw new NotFoundException("Announcement not found");

                FileDto? fileDto = await _fileStorage.GetFileAsync(request.FileName, request.FileData);

                if (fileDto == null)
                    throw new NotFoundException("Image not found");

                File image = new File(fileDto.Name, fileDto.Extension, fileDto.Path);

                if (user.IsAdmin || user.Id == announcement.Id)
                {
                    //TODO проверка на уникальность Number
                    announcement.Number = request.Number;
                    announcement.Text = request.Text;
                    announcement.Image = image;
                    announcement.Rating = request.Rating;
                    announcement.ExpirationDate = request.ExpirationDate;

                    await _announcementsUnitOfWork.Repository<Announcement>()
                        .UpdateAsync(announcement, cancellationToken);
                    await _announcementsUnitOfWork.SaveAsync(cancellationToken);
                }
                else
                    throw new BadRequestException("To update or remove announcements can only admin or owner.");

                return await Task.FromResult(new Result<Guid>(request.Id, "Announcement was updated"));
            });
        }
    }
}
