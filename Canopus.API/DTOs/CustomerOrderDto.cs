using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.DTOs;

[ExcludeFromCodeCoverage]
public class CustomerOrderDto
{
    public CustomerOrderDto(CustomerDto customer, IList<OrderDto> orders)
    {
        Customer = customer;
        Orders = orders;
    }

    public CustomerDto Customer { get; }

    public IList<OrderDto> Orders { get; }
}