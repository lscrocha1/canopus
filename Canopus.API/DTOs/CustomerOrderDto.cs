namespace Canopus.API.DTOs;

public class CustomerOrderDto
{
    public CustomerOrderDto(CustomerDto customer, IList<OrderDto> orders)
    {
        Customer = customer;
        Orders = orders;
    }

    public CustomerDto Customer { get; set; }

    public IList<OrderDto> Orders { get; set; }
}