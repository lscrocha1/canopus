using Canopus.API.DTOs;
using Canopus.API.Infrastructure.Context;
using Canopus.API.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Canopus.API.Services.Domain.Order;

public class OrderService : IOrderService
{
    private readonly CanopusContext _context;

    public OrderService(CanopusContext context)
    {
        _context = context;
    }

    public async Task<CustomerOrderDto> GetByCustomerId(Guid customerId, CancellationToken token)
    {
        var customer = await _context
            .Set<API.Domain.Customer>()
            .AsNoTracking()
            .Include(e => e.Orders)
            .FirstOrDefaultAsync(e => e.Id == customerId, token)
            .ConfigureAwait(false);

        if (customer is null)
            throw new NotFoundException("Customer not found");

        return new CustomerOrderDto(customer);
    }

    public async Task Add(Guid customerId, decimal price, CancellationToken token)
    {
        var customer = await _context
            .Set<API.Domain.Customer>()
            .Include(e => e.Orders)
            .FirstOrDefaultAsync(e => e.Id == customerId, token)
            .ConfigureAwait(false);

        if (customer is null)
            throw new NotFoundException("Customer not found");

        customer.Orders ??= new List<API.Domain.Order>();
        
        customer.Orders.Add(new API.Domain.Order
        {
            Price = price,
            CreatedAt = DateTime.Now
        });

        await _context.SaveChangesAsync(token).ConfigureAwait(false);
    }
}