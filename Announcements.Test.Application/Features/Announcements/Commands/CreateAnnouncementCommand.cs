using Announcements.Test.Application.Common.Exceptions;
using Announcements.Test.Application.DTO;
using Announcements.Test.Application.Interfaces;
using Announcements.Test.Application.Interfaces.Repositories;
using Announcements.Test.Application.Options;
using Announcements.Test.Domain.Entities;
using Announcements.Test.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using File = Announcements.Test.Domain.Common.ValueObjects.File;  

namespace Announcements.Test.Application.Features.Announcements.Commands
{
    public class CreateAnnouncementCommand : IRequest<Result<Guid>>
    {
        public Guid UserId { get; set; }

        public int Number { get; set; }
        
        public string Text { get; set; } = string.Empty;

        public DateOnly ExpirationDate { get; set; }

        public int Rating { get; set; }

        public ImageDto Image { get; set; } = null!;
    }

    internal class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _announcementsUnitOfWork;
        private readonly IUnitOfWork _usersUnitOfWork;
        private readonly IFileStorage _fileStorage;
        private readonly AnnouncementOptions _announcementOptions;


        public CreateAnnouncementCommandHandler(IUnitOfWork announcementsUnitOfWork, IUnitOfWork usersUnitOfWork, IFileStorage fileStorage, IOptions<AnnouncementOptions> announcementOptions)
        {
            _announcementsUnitOfWork = announcementsUnitOfWork;
            _usersUnitOfWork = usersUnitOfWork;
            _fileStorage = fileStorage;
            _announcementOptions = announcementOptions.Value;
        }

        public async Task<Result<Guid>> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var query = _usersUnitOfWork.Repository<User>().Entities;
            query = query.Include(x => x.Announcements);

            User ? user = await query.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user == null)
                throw new NotFoundException("User not found");

            if (user.Announcements.Count >= _announcementOptions.UserLimit)
                throw new BadRequestException($"User can't create more then {_announcementOptions.UserLimit} announcements.");

            if(request.Image.FileData == null)
                throw new BadRequestException($"File can't be empty");

            FileDto? fileDto = await _fileStorage.SaveFileAsync(request.Image.FileName, request.Image.FileData);

            if (fileDto == null)
                throw new NotFoundException("Image not found");

            File image = new File(fileDto.Name, fileDto.Extension, fileDto.Path);

            DateTime expirationDate = request.ExpirationDate.ToDateTime(TimeOnly.MinValue);

            Announcement announcement =
                new Announcement(user, request.Number, request.Text, image, request.Rating, expirationDate);

            await _announcementsUnitOfWork.Repository<Announcement>().AddAsync(announcement, cancellationToken);
            await _announcementsUnitOfWork.SaveAsync(cancellationToken);

            return await Task.FromResult(new Result<Guid>(announcement.Id, "Announcement was created"));
        }
    }
}
