using Announcements.Test.Application.DTO;
using AutoMapper;
using File = Announcements.Test.Domain.Common.ValueObjects.File;

namespace Announcements.Test.Application.Mapping.Profiles
{
    internal class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<File, FileDto>();
            CreateMap<FileDto, File>();
            CreateMap<File, ImageDto>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.FileData, opt => opt.Ignore());
        }
    }
}
