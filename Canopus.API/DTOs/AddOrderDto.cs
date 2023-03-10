using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.DTOs;

[ExcludeFromCodeCoverage]
public class AddOrderDto
{
    public AddOrderDto(decimal price)
    {
        Price = price;
    }

    [Required]
    public decimal Price { get; }
}