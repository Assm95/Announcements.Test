using Announcements.EF.Const;
using Announcements.EF.Converters;
using Microsoft.EntityFrameworkCore;
using Announcements.EF.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Announcements.EF.Configurations
{
    internal class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.Text)
                .HasMaxLength(AnnouncementConst.TextMaxLength)
                .IsRequired();
            
            builder.HasOne(x => x.User)
                .WithMany(x => x.Announcements)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.OwnsOne(x => x.Image, FileConfiguration.Default);

            builder.Property(x => x.ExpirationDate)
                .HasConversion<DateTimeConverter>()
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasConversion<DateTimeConverter>()
                .IsRequired();

            builder.HasIndex(x => x.Number)
                .IsUnique();
        }
    }
}
