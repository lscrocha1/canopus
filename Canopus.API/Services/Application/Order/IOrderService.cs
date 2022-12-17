using Canopus.API.DTOs;
using Canopus.API.Responses;

namespace Canopus.API.Services.Application.Order;

public interface IOrderService
{
    Task<SingleResponse<CustomerOrderDto>> GetCustomerOrders(Guid customerId);

    Task Add(Guid customerId, AddOrderDto dto);
}