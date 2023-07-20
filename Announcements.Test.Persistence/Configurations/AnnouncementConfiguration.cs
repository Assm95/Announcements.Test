using Announcements.Test.Domain.Entities;
using Announcements.Test.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Announcements.Test.Persistence.Configurations
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
                .HasMaxLength(2000)
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

            builder.HasIndex(x => x.CreatedAt);
            builder.HasIndex(x => x.ExpirationDate);
            builder.HasIndex(x => x.Rating);
        }
    }
}
