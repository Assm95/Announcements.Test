using Announcements.Test.Application.DTO;
using Announcements.Test.Domain.Entities;
using AutoMapper;

namespace Announcements.Test.Application.Mapping.Profiles
{
    internal class AnnouncementProfile : Profile
    {
        public AnnouncementProfile()
        {
            CreateMap<Announcement, AnnouncementDto>();
        }
    }
}
