using Announcements.Test.Application.Common.Exceptions;
using Announcements.Test.Application.DTO;
using Announcements.Test.Application.Interfaces;
using Announcements.Test.Application.Interfaces.Repositories;
using Announcements.Test.Domain.Entities;
using Announcements.Test.Shared;
using MediatR;
using File = Announcements.Test.Domain.Common.ValueObjects.File;  

namespace Announcements.Test.Application.Features.Announcements.Commands
{
    public class CreateAnnouncementCommand : IRequest<Result<Guid>>
    {
        public Guid UserId { get; set; }

        public int Number { get; set; }
        
        public string Text { get; set; } = string.Empty;

        public DateTime ExpirationDate { get; set; }

        public int Rating { get; set; }

        public string FileName { get; set; } = string.Empty;

        public byte[] FileData { get; set; } = Array.Empty<byte>();
    }

    internal class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _announcementsUnitOfWork;
        private readonly IUnitOfWork _usersUnitOfWork;
        private readonly IFileStorage _fileStorage;


        public CreateAnnouncementCommandHandler(IUnitOfWork announcementsUnitOfWork, IUnitOfWork usersUnitOfWork, IFileStorage fileStorage)
        {
            _announcementsUnitOfWork = announcementsUnitOfWork;
            _usersUnitOfWork = usersUnitOfWork;
            _fileStorage = fileStorage;
        }

        public async Task<Result<Guid>> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
           return await ExceptionWrapper<Result<Guid>>.Catch(async () =>
            {
                User? user = await _usersUnitOfWork.Repository<User>().GetByIdAsync(request.UserId, cancellationToken);

                if (user == null)
                    throw new NotFoundException("User not found");

                FileDto? fileDto = await _fileStorage.GetFileAsync(request.FileName, request.FileData);

                if(fileDto == null)
                    throw new NotFoundException("Image not found");

                File image = new File(fileDto.Name, fileDto.Extension, fileDto.Path);

                Announcement announcement =
                    new Announcement(user, request.Number, request.Text, image, request.Rating, request.ExpirationDate);

                await _announcementsUnitOfWork.Repository<Announcement>().AddAsync(announcement, cancellationToken);
                await _announcementsUnitOfWork.SaveAsync(cancellationToken);

                return await Task.FromResult(new Result<Guid>(announcement.Id, "Announcement was created"));
            });
        }
    }
}
