using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Announcements.Test.Domain.Common.ValueObjects.File;

namespace Announcements.Test.Persistence.Configurations
{
    public static class FileConfiguration
    {
        public static void Default<TEntity>(OwnedNavigationBuilder<TEntity, File> ownedBuilder)
            where TEntity : class
        {
            ownedBuilder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            ownedBuilder.Property(x => x.Extension)
                .HasMaxLength(255)
                .IsRequired(false);

            ownedBuilder.Property(x => x.Path)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
