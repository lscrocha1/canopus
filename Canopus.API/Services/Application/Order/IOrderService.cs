using Canopus.API.DTOs;
using Canopus.API.Responses;

namespace Canopus.API.Services.Application.Order;

public interface IOrderService
{
    Task<SingleResponse<CustomerOrderDto>> GetCustomerOrders(
        Guid customerId, 
        CancellationToken token);

    Task Add(
        Guid customerId, 
        AddOrderDto dto, 
        CancellationToken token);
}