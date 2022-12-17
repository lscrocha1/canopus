using System.Diagnostics.CodeAnalysis;
using Canopus.API.Domain;

namespace Canopus.API.DTOs;

[ExcludeFromCodeCoverage]
public class CustomerOrderDto
{
    public CustomerOrderDto(CustomerDto customer, IList<OrderDto> orders)
    {
        Customer = customer;
        Orders = orders;
    }

    public CustomerOrderDto(Customer customer)
    {
        Customer = new CustomerDto(customer.Id, customer.Name, customer.Email);
        
        Orders = customer
            .Orders
            .Select(e => new OrderDto(e.Price, e.CreatedAt))
            .ToList();
    }

    public CustomerDto Customer { get; }

    public IList<OrderDto> Orders { get; }
}