using Announcements.EF.Const;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Announcements.EF.Models.File;

namespace Announcements.EF.Configurations
{
    public static class FileConfiguration
    {
        public static void Default<TEntity>(OwnedNavigationBuilder<TEntity, File> ownedBuilder)
            where TEntity : class
        {
            ownedBuilder.Property(x => x.Name)
                .HasMaxLength(FileConst.NameMaxLength)
                .IsRequired();

            ownedBuilder.Property(x => x.Extension)
                .HasMaxLength(FileConst.ExtensionMaxLength)
                .IsRequired(false);

            ownedBuilder.Property(x => x.Data)
                .IsRequired();
        }
    }
}
