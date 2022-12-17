using Canopus.API.DTOs;

namespace Canopus.API.Services.Domain.Order;

public interface IOrderService
{
    Task<CustomerOrderDto> GetByCustomerId(Guid customerId);
}