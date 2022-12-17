using System.Diagnostics.CodeAnalysis;
using Canopus.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Canopus.API.Infrastructure.EntityConfiguration;

[ExcludeFromCodeCoverage]
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(256);

        builder.Property(e => e.Email).HasMaxLength(256);

        builder
            .HasMany(e => e.Orders)
            .WithOne(e => e.Customer)
            .OnDelete(DeleteBehavior.Cascade);
    }
}