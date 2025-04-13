using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reels.Backoffice.Domain.Models.Video;

namespace Reels.Backoffice.Persistence.Mappings.VideoMapping;

internal sealed class VideoCategoryMapping : IEntityTypeConfiguration<VideoCategory>
{
    public void Configure(EntityTypeBuilder<VideoCategory> builder)
    {
        builder.ToTable(nameof(VideoCategory));

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id)
            .IsUnique();

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.VideoId)
            .IsRequired();

        builder.Property(x => x.CategoryId)
            .IsRequired();
    }
}