using Announcements.Test.Application.DTO;
using Announcements.Test.Domain.Entities;
using AutoMapper;

namespace Announcements.Test.Application.Mapping.Profiles
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
