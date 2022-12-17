using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.DTOs;

[ExcludeFromCodeCoverage]
public class OrderDto
{
    public OrderDto(decimal price, DateTime createdAt)
    {
        Price = price;
        CreatedAt = createdAt;
    }

    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }
}