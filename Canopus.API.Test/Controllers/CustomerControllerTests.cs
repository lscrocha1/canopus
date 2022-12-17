using Canopus.API.Controllers;
using Canopus.API.DTOs;
using Canopus.API.Responses;
using Canopus.API.Services.Application.Customer;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Canopus.API.Test.Controllers;

public class CustomerControllerTests
{
    [Fact]
    public async Task Get_Should_Return_204_When_Theres_No_Customer_Returned()
    {
        var customerServiceMock = new Mock<ICustomerService>();

        customerServiceMock
            .Setup(e => e.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>(), It.IsAny<string>()))
            .ReturnsAsync(new MultipleResponse<CustomerDto>(0, 0, 0, Array.Empty<CustomerDto>()));

        var controller = new CustomerController(customerServiceMock.Object);

        var result = await controller.Get(Helpers.GetCancellationToken());

        Assert.IsType<NoContentResult>(result.Result);
    }

    [Fact]
    public async Task Get_Should_Return_200_When_One_Or_More_Customer_Returns()
    {
        var customerServiceMock = new Mock<ICustomerService>();

        customerServiceMock
            .Setup(e => e.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>(), It.IsAny<string>()))
            .ReturnsAsync(
                new MultipleResponse<CustomerDto>(0, 0, 0, new List<CustomerDto>
                {
                    new(Guid.NewGuid(), string.Empty, string.Empty)
                }));

        var controller = new CustomerController(customerServiceMock.Object);

        var result = await controller.Get(Helpers.GetCancellationToken());

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task AddCustomer_Should_Return_201()
    {
        var customerServiceMock = new Mock<ICustomerService>();

        customerServiceMock
            .Setup(e => e.AddCustomer(new AddCustomerDto(string.Empty, string.Empty), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CustomerDto(Guid.Empty, string.Empty, string.Empty));

        var controller = new CustomerController(customerServiceMock.Object);

        var dto = new AddCustomerDto(string.Empty, string.Empty);
        var token = Helpers.GetCancellationToken();

        var result = await controller.Add(dto, token);

        result.Should().NotBeNull();
        (result.Result as ObjectResult)!.StatusCode.Should().Be(201);
    }
}