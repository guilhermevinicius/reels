using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reels.Backoffice.Domain.Models.Video;

namespace Reels.Backoffice.Persistence.Mappings.VideoMapping;

internal sealed class MediaMapping : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.ToTable(nameof(Media));
        
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id)
            .IsUnique();

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.Property(x => x.FilePath)
            .IsRequired();

        builder.Property(x => x.EncodedPath);

        builder.Property(x => x.Status)
            .IsRequired();
    }
}