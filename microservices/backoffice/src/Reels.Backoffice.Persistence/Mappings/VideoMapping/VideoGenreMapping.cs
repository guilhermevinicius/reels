using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reels.Backoffice.Domain.Models.Video;

namespace Reels.Backoffice.Persistence.Mappings.VideoMapping;

internal sealed class VideoGenreMapping : IEntityTypeConfiguration<VideoGenre>
{
    public void Configure(EntityTypeBuilder<VideoGenre> builder)
    {
        builder.ToTable(nameof(VideoGenre));

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id)
            .IsUnique();

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.VideoId)
            .IsRequired();

        builder.Property(x => x.GenreId)
            .IsRequired();
    }
}