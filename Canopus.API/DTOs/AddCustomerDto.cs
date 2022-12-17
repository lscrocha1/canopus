using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.DTOs;

[ExcludeFromCodeCoverage]
public class AddCustomerDto
{
    public AddCustomerDto(string name, string email)
    {
        Name = name;
        Email = email;
    }

    [Required]
    [MaxLength(256)]
    public string Name { get; }

    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; }
}