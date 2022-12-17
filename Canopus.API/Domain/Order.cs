using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Domain;

[ExcludeFromCodeCoverage]
public class Order
{
    public Guid Id { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;
}