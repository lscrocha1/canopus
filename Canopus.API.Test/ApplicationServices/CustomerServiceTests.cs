using Canopus.API.DTOs;
using Canopus.API.Services.Application.Customer;
using FluentAssertions;

namespace Canopus.API.Test.ApplicationServices;

public class CustomerServiceTests
{
    [Fact]
    public async Task Get_Should_Return_Empty_And_TotalItems_Should_Be_0()
    {
        var customerService = new CustomerService(new Services.Domain.Customer.CustomerService(Helpers.GetDbContext()));

        var result = await customerService.Get(pageIndex: 1, pageSize: 20, Helpers.GetCancellationToken());

        result.Data.Items.Should().BeEmpty();
        result.Data.TotalItems.Should().Be(0);
    }

    [Fact]
    public async Task Add_Should_Be_Fetched_By_Get()
    {
        var customerService = new CustomerService(new Services.Domain.Customer.CustomerService(Helpers.GetDbContext()));

        var customerName = Guid.NewGuid().ToString();

        var customer = await customerService.AddCustomer(
            new AddCustomerDto(customerName, Guid.NewGuid().ToString()), 
            Helpers.GetCancellationToken());

        var result = await customerService.Get(
            pageIndex: 1, 
            pageSize: 20, 
            Helpers.GetCancellationToken());

        result.Should().NotBeNull();
        result.Data.TotalItems.Should().Be(1);
        result.Data.Items.FirstOrDefault()!.Email.Should().Be(customer.Data!.Email);
    }
}