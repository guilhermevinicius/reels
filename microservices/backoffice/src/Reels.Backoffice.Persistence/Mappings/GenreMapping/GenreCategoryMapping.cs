using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reels.Backoffice.Domain.Models.Genre;

namespace Reels.Backoffice.Persistence.Mappings.GenreMapping;

internal sealed class GenreCategoryMapping : IEntityTypeConfiguration<GenreCategory>
{
    public void Configure(EntityTypeBuilder<GenreCategory> builder)
    {
        builder.ToTable(nameof(GenreCategory));
        
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id)
            .IsUnique();

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasIndex(x => new { x.GenreId, x.CategoryId });
        
        builder.Property(x => x.GenreId)
            .IsRequired();

        builder.Property(x => x.CategoryId)
            .IsRequired();
    }
}