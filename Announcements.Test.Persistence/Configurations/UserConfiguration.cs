using Announcements.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Announcements.Test.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.IsAdmin)
                .IsRequired();
        }
    }
}
