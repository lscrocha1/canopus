using Canopus.API.Domain;
using Canopus.API.DTOs;
using Canopus.API.Services.Application.Order;
using FluentAssertions;
using Moq;
using IOrderService = Canopus.API.Services.Domain.Order.IOrderService;

namespace Canopus.API.Test.ApplicationServices;

public class OrderServiceTests
{
    [Fact]
    public async Task Get_Should_Return_Exact_Quantity_As_DomainService()
    {
        var orderServiceMock = new Mock<IOrderService>();

        orderServiceMock
            .Setup(e => e.GetByCustomerId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CustomerOrderDto(new Customer()
            {
                Orders = new List<Order>()
                {
                    new(),
                    new(),
                    new()
                }
            }));

        var orderService = new OrderService(orderServiceMock.Object);

        var result = await orderService.GetCustomerOrders(Guid.NewGuid(), Helpers.GetCancellationToken());

        result.Should().NotBeNull();
        result.Data!.Orders.Count.Should().Be(3);
    }

    [Fact]
    public async Task Add_Should_Be_Able_To_Be_Fetched_By_Get()
    {
        var context = Helpers.GetDbContext();

        var customer = new Customer();

        await context.AddAsync(customer);

        await context.SaveChangesAsync();
        
        var orderService = new OrderService(new Services.Domain.Order.OrderService(context));

        await orderService.Add(customer.Id, new AddOrderDto(100), Helpers.GetCancellationToken());

        var result = await orderService.GetCustomerOrders(customer.Id, Helpers.GetCancellationToken());

        result.Data!.Orders.Count.Should().Be(1);
        result.Data.Orders.FirstOrDefault()!.Price.Should().Be(100);
    }
}