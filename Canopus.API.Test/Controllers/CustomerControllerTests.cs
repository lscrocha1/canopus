using Canopus.API.Controllers;
using Canopus.API.DTOs;
using Canopus.API.Services.Domain.Customer;
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
            .Setup(e => e.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(new List<CustomerDto>());

        var controller = new CustomerController(customerServiceMock.Object);

        var result = await controller.Get();

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Get_Should_Return_200_When_One_Or_More_Customer_Returns()
    {
        var customerServiceMock = new Mock<ICustomerService>();

        customerServiceMock
            .Setup(e => e.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(new List<CustomerDto>()
            {
                new(Guid.NewGuid(), "", "")
            });

        var controller = new CustomerController(customerServiceMock.Object);

        var result = await controller.Get();

        Assert.IsType<OkObjectResult>(result);
    }
}