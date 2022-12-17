using Canopus.API.Controllers;
using Canopus.API.DTOs;
using Canopus.API.Responses;
using Canopus.API.Services.Application.Order;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Canopus.API.Test.Controllers;

public class OrderControllerTests
{
    [Fact]
    public async Task GetCustomerOrders_Should_Return_404()
    {
        var orderServiceMock = new Mock<IOrderService>();

        orderServiceMock
            .Setup(e => e.GetCustomerOrders(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SingleResponse<CustomerOrderDto>(null));

        var controller = new OrderController(orderServiceMock.Object);

        var result = await controller.GetOrdersByCustomerId(Guid.NewGuid(), Helpers.GetCancellationToken());

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetCustomerOrders_Should_Return_200()
    {
        var orderServiceMock = new Mock<IOrderService>();

        orderServiceMock
            .Setup(e => e.GetCustomerOrders(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SingleResponse<CustomerOrderDto>(
                new CustomerOrderDto(
                    new CustomerDto(Guid.Empty, string.Empty, string.Empty),
                    new List<OrderDto>())));

        var controller = new OrderController(orderServiceMock.Object);

        var result = await controller.GetOrdersByCustomerId(Guid.NewGuid(), Helpers.GetCancellationToken());

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task AddOrder_Should_Return_201()
    {
        var orderServiceMock = new Mock<IOrderService>();

        var controller = new OrderController(orderServiceMock.Object);

        var result = await controller.Add(Guid.Empty, new AddOrderDto(0), Helpers.GetCancellationToken());

        (result as StatusCodeResult)!.StatusCode.Should().Be(201);
    }
}