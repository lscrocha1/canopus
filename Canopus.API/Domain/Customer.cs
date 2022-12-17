using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Domain;

[ExcludeFromCodeCoverage]
public class Customer
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public IList<Order> Orders { get; set; } = ArraySegment<Order>.Empty;
}