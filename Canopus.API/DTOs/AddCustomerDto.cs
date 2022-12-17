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
    public string Name { get; }

    [Required]
    [EmailAddress]
    public string Email { get; }
}