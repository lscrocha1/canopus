using Canopus.API.Domain;
using Canopus.API.Infrastructure.Exceptions;
using Canopus.API.Services.Domain.Order;
using FluentAssertions;

namespace Canopus.API.Test.DomainServices;

public class OrderServiceTests
{
    [Fact]
    public async Task GetByCustomerId_Should_Throw_NotFoundException_If_Customer_Is_Not_Found()
    {
        var orderService = new OrderService(Helpers.GetDbContext());

        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await orderService.GetByCustomerId(Guid.NewGuid(), Helpers.GetCancellationToken()));
    }

    [Fact]
    public async Task GetByCustomerId_Should_Return_Customer_And_Orders()
    {
        var context = Helpers.GetDbContext();

        var orderService = new OrderService(context);

        var customer = new Customer()
        {
            Orders = new List<Order>()
            {
                new(),
                new()
            }
        };

        await context.AddAsync(customer);

        await context.SaveChangesAsync();

        var result = await orderService.GetByCustomerId(customer.Id, Helpers.GetCancellationToken());

        result.Should().NotBeNull();
        result.Orders.Count.Should().Be(2);
    }
    
    [Fact]
    public async Task Add_Should_Throw_NotFoundException_If_Customer_Is_Not_Found()
    {
        var orderService = new OrderService(Helpers.GetDbContext());

        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await orderService.Add(Guid.NewGuid(), 0, Helpers.GetCancellationToken()));
    }

    [Fact]
    public async Task Add_Should_AddOrder_And_Should_Be_Retrieved()
    {
        var context = Helpers.GetDbContext();

        var orderService = new OrderService(context);

        var customer = new Customer();

        await context.AddAsync(customer);

        await context.SaveChangesAsync();

        await orderService.Add(customer.Id, 0, Helpers.GetCancellationToken());

        var result = await orderService.GetByCustomerId(customer.Id, Helpers.GetCancellationToken());

        result.Should().NotBeNull();
        result.Orders.Count.Should().Be(1);
    }
}