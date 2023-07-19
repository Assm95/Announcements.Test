using System.Diagnostics.CodeAnalysis;
using Announcements.EF.Exceptions;
using Announcements.EF.Filters;
using Announcements.EF.Models;
using Announcements.WebApi.Models;
using Announcements.WebApi.Queries;
using File = Announcements.EF.Models.File;

namespace Announcements.WebApi
{
    internal static class Mapping
    {
        [return: NotNullIfNotNull("value")]
        public static AnnouncementDto? ToDto(this Announcement? value, bool includeImage = true)
        {
            return value != null
                ? new AnnouncementDto
                {
                    Id = value.Id,
                    CreatedAt = value.CreatedAt,
                    ExpirationDate = DateOnly.FromDateTime(value.ExpirationDate),
                    Image = includeImage ? value.Image.ToDto() : null,
                    Number = value.Number,
                    Rating = value.Rating,
                    Text = value.Text,
                    User = value.User.ToDto()
                }
                : null;
        }

        [return: NotNullIfNotNull("value")]
        public static UserDto? ToDto(this User? value)
        {
            return value != null 
                ? new UserDto
                {
                    Id = value.Id,
                    Name = value.Name,
                    IsAdmin = value.IsAdmin
                } : null;
        }

        [return: NotNullIfNotNull("value")]
        public static FileDto? ToDto(this File? value)
        {
            return value != null
                ? new FileDto
                {
                    Data = value.Data,
                    Name = value.NameWithExtension
                }
                : null;
        }

        public static File ToDomain(this FileDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new DomainException("File name is not set");

            string name = Path.GetFileNameWithoutExtension(dto.Name);
            string? extension = Path.GetExtension(dto.Name).TrimStart('.');
            
            return new File(name, extension, dto.Data);
        }

        [return: NotNullIfNotNull("query")]
        public static AnnouncementsFilter? ToFilter(this GetAnnouncementsFilteringQuery? query)
        {
            return query != null
                ? new AnnouncementsFilter
                {
                    UserId = query.UserId,
                    CreatedAtStart = query.CreatedAtStart,
                    CreatedAtEnd = query.CreatedAtEnd,
                    Rating = query.Rating,
                    ExpirationDateStart = query.ExpirationDateStart,
                    ExpirationDateEnd = query.ExpirationDateEnd,
                    Number = query.Number
                }
                : null;
        }

        [return: NotNullIfNotNull("query")]
        public static PaginationFilter? ToFilter(this PaginationQuery? query)
        {
            return query != null
                ? new PaginationFilter
                {
                    PageCount = query.PageCount,
                    PageStart = query.PageStart
                }
                : null;
        }

        [return: NotNullIfNotNull("query")]
        public static SortingFilter? ToFilter(this GetAnnouncementsSortingQuery? query)
        {
            return query != null
                ? new SortingFilter
                {
                    ColumnName = query.ColumnName,
                    SortOrder = query.SortOrder
                }
                : null;
        }
    }
}
