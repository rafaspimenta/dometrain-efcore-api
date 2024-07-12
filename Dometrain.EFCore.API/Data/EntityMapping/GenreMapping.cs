using Dometrain.EFCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dometrain.EFCore.API.Data.EntityMapping;

public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(movie => movie.Name)
            .HasColumnType("varchar")
            .HasMaxLength(128)
            .IsRequired();

        builder.HasData(new Genre("Action", 1));
    }
}
