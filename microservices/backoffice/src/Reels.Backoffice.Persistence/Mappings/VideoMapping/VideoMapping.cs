using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reels.Backoffice.Domain.Models.Video;

namespace Reels.Backoffice.Persistence.Mappings.VideoMapping;

internal sealed class VideoMapping : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.ToTable(nameof(Video));

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id)
            .IsUnique();

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.YearLaunched)
            .IsRequired();

        builder.Property(x => x.Opened)
            .IsRequired();

        builder.Property(x => x.Published)
            .IsRequired();

        builder.Property(x => x.Rating)
            .IsRequired();

        builder.OwnsOne(x => x.Thumb, thumb =>
        {
            thumb.Property(x => x.Path)
                .HasColumnName("ThumbPath");
        });

        builder.OwnsOne(x => x.ThumbHalf, thumbHalf =>
        {
            thumbHalf.Property(x => x.Path)
                .HasColumnName("thumbHalfPath");
        });

        builder.OwnsOne(x => x.Banner, banner =>
        {
            banner.Property(x => x.Path)
                .HasColumnName("BannerPath");
        });

        builder.HasOne(x => x.Media);

        builder.HasOne(x => x.Trailer);

        builder.HasMany(x => x.Categories);

        builder.HasMany(x => x.Genres);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Ignore(x => x.Events);
    }
}