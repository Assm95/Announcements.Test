using Announcements.Test.Application.DTO;
using Announcements.Test.Domain.Entities;
using AutoMapper;

namespace Announcements.Test.Application.Mapping.Profiles
{
    internal class AnnouncementProfile : Profile
    {
        public AnnouncementProfile()
        {
            CreateMap<Announcement, AnnouncementDto>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(x =>
                        !string.IsNullOrWhiteSpace(x.Image.Path) ? new Uri(x.Image.Path).AbsoluteUri : null));
        }
    }
}
