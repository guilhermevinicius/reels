using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reels.Backoffice.Domain.Models.Genre;

namespace Reels.Backoffice.Persistence.Mappings.GenreMapping;

internal sealed class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable(nameof(Genre));

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id)
            .IsUnique();

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasMany(x => x.GenreCategories)
            .WithOne(x => x.Genre)
            .HasForeignKey(x => x.GenreId);

        builder.Ignore(x => x.Events);
    }
}