using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.DTOs;

[ExcludeFromCodeCoverage]
public class CustomerDto
{
    public CustomerDto(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }
}