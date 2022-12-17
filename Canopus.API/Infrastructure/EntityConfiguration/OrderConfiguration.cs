using System.Diagnostics.CodeAnalysis;
using Canopus.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Canopus.API.Infrastructure.EntityConfiguration;

[ExcludeFromCodeCoverage]
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Price).HasColumnType("decimal(18, 2)");
    }
}