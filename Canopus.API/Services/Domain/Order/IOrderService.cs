using Canopus.API.DTOs;

namespace Canopus.API.Services.Domain.Order;

public interface IOrderService
{
    Task<CustomerOrderDto> GetByCustomerId(Guid customerId, CancellationToken token);

    Task Add(decimal price, CancellationToken token);
}