using System.Text.Json.Serialization;
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
        [JsonIgnore]
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateOnly ExpirationDate { get; set; }

        public int Rating { get; set; }

        public Guid UserId { get; set; }

        public ImageDto Image { get; set; } = null!;
    }

    internal class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _announcementsUnitOfWork;
        private readonly IUnitOfWork _usersUnitOfWork;
        private readonly IFileStorage _fileStorage;
        private readonly IAnnouncementsRepository _announcementsRepository;

        public UpdateAnnouncementCommandHandler(IUnitOfWork announcementsUnitOfWork, IUnitOfWork usersUnitOfWork,
            IFileStorage fileStorage, IAnnouncementsRepository announcementsRepository)
        {
            _announcementsUnitOfWork = announcementsUnitOfWork;
            _usersUnitOfWork = usersUnitOfWork;
            _fileStorage = fileStorage;
            _announcementsRepository = announcementsRepository;
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

                await CheckUniqueNumberAsync(announcement, request.Number, cancellationToken);

                FileDto? fileDto = await _fileStorage.GetFileAsync(request.Image.FileName, request.Image.FileData);

                if (fileDto == null)
                    throw new NotFoundException("Image not found");

                File image = new File(fileDto.Name, fileDto.Extension, fileDto.Path);
                DateTime expirationDate = request.ExpirationDate.ToDateTime(TimeOnly.MinValue);
                
                if (user.IsAdmin || user.Id == announcement.Id)
                {
                    announcement.Number = request.Number;
                    announcement.Text = request.Text;
                    announcement.Image = image;
                    announcement.Rating = request.Rating;
                    announcement.ExpirationDate = expirationDate;

                    await _announcementsUnitOfWork.Repository<Announcement>()
                        .UpdateAsync(announcement, cancellationToken);
                    await _announcementsUnitOfWork.SaveAsync(cancellationToken);
                }
                else
                    throw new BadRequestException("To update or remove announcements can only admin or owner.");

                return await Task.FromResult(new Result<Guid>(request.Id, "Announcement was updated"));
            });
        }

        private async Task CheckUniqueNumberAsync(Announcement announcement, int newNumber, CancellationToken cancellationToken)
        {
            if (announcement.Number != newNumber)
            {
                bool numberExist = await _announcementsRepository.FindByNumberAsync(newNumber, cancellationToken) != null;

                if (numberExist)
                    throw new BadRequestException("The announcement number must be unique.");
            }
        }
    }
}
