using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Announcements.Test.Persistence.Converters
{
    internal class DateTimeConverter : ValueConverter<DateTime, DateTime>
    {
        public DateTimeConverter() : base(
            src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
            dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            )
        {
            
        }
    }
}
