using Canopus.API.DTOs;
using Canopus.API.Responses;

namespace Canopus.API.Services.Application.Order;

public class OrderService : IOrderService
{
    private readonly Domain.Order.IOrderService _orderService;

    public OrderService(Domain.Order.IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<SingleResponse<CustomerOrderDto>> GetCustomerOrders(Guid customerId, CancellationToken token)
    {
        var result = await _orderService.GetByCustomerId(customerId, token);

        return new SingleResponse<CustomerOrderDto>(result);
    }

    public async Task Add(Guid customerId, AddOrderDto dto, CancellationToken token)
    {
        await _orderService.Add(customerId, dto.Price, token);
    }
}