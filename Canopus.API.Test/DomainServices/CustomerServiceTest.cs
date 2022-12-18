using Canopus.API.Domain;
using Canopus.API.DTOs;
using Canopus.API.Infrastructure.Context;
using Canopus.API.Infrastructure.Exceptions;
using Canopus.API.Services.Domain.Customer;
using FluentAssertions;

namespace Canopus.API.Test.DomainServices;

public class CustomerServiceTest
{
    [Fact]
    public async Task Get_Should_Return_Maximum_Length_Of_PageSize()
    {
        var context = Helpers.GetDbContext();
        
        var customerService = new CustomerService(context);

        await AddCustomers(quantity: 100, context);

        var pageSize = new Random().Next(1, 21);

        var result = await customerService.Get(pageIndex: 1, pageSize, Helpers.GetCancellationToken());

        result.Should().NotBeNull(); 
        
        result.Customers.Count().Should().Be(pageSize);             
        result.PaginationDto.TotalItems.Should().Be(100);
    }

    [Fact]
    public async Task Get_Should_Return_Only_Searched_Items()
    {
        var context = Helpers.GetDbContext();
        
        var customerService = new CustomerService(context);

        await AddCustomers(quantity: 50, context);

        var customerName = Guid.NewGuid().ToString();

        await context.AddAsync(new Customer
        {
            Name = customerName
        });

        await context.SaveChangesAsync();

        var result = await customerService.Get(
            pageIndex: 1, 
            pageSize: 20, 
            Helpers.GetCancellationToken(),
            search: customerName);

        result.Should().NotBeNull(); 
        
        result.Customers.Count().Should().Be(1);             
        result.PaginationDto.TotalItems.Should().Be(1);
    }

    [Fact]
    public async Task Get_Should_Throw_Exception_When_PageSize_Is_Less_Than_One()
    {
        var customerService = new CustomerService(Helpers.GetDbContext());

        await Assert.ThrowsAsync<BadRequestException>(async () =>
            await customerService.Get(pageIndex: 1, pageSize: 0, Helpers.GetCancellationToken()));
    }

    [Fact]
    public async Task Get_Should_Throw_Exception_When_PageIndex_Is_Less_Than_One()
    {
        var customerService = new CustomerService(Helpers.GetDbContext());

        await Assert.ThrowsAsync<BadRequestException>(async () =>
            await customerService.Get(pageIndex: 0, pageSize: 1, Helpers.GetCancellationToken()));
    }
    
    [Fact]
    public async Task Get_Should_Return_Same_PageSize_PageIndex()
    {
        var context = Helpers.GetDbContext();
        
        var customerService = new CustomerService(context);

        await AddCustomers(quantity: 100, context);

        var pageSize = new Random().Next(1, 21);

        var pageIndex = 1;

        var result = await customerService.Get(pageIndex, pageSize, Helpers.GetCancellationToken());

        result.Should().NotBeNull(); 
        
        result.Customers.Count().Should().Be(pageSize);             
        result.PaginationDto.TotalItems.Should().Be(100);
        result.PaginationDto.PageIndex.Should().Be(pageIndex);
        result.PaginationDto.PageSize.Should().Be(pageSize);
    }

    [Fact]
    public async Task Add_Should_Throw_Exception_If_Email_Already_Exists()
    {
        var context = Helpers.GetDbContext();
        
        var customerService = new CustomerService(context);

        var customerEmail = "abcdefgijk@outlook.com";

        await context.AddAsync(new Customer
        {
            Email = customerEmail
        });

        await context.SaveChangesAsync();
        
        await Assert.ThrowsAsync<UnprocessableEntityException>(async () => 
            await customerService.Add(
            new AddCustomerDto(name: Guid.NewGuid().ToString(), customerEmail),
            Helpers.GetCancellationToken()));
    }

    [Fact]
    public async Task Add_Should_AddCustomer_And_Should_Be_Retrieved_By_Get_By_Email_And_Name()
    {
        var context = Helpers.GetDbContext();
        
        var customerService = new CustomerService(context);

        var customerName = Guid.NewGuid().ToString();

        var customerEmail = Guid.NewGuid().ToString();

        var result = await customerService.Add(
            new AddCustomerDto(customerName, customerEmail), 
            Helpers.GetCancellationToken());

        result.Email.Should().Be(customerEmail);
        result.Name.Should().Be(customerName);

        var customerByName = await customerService.Get(
            pageIndex: 1, 
            pageSize: 20, 
            Helpers.GetCancellationToken(), 
            customerName);

        customerByName.Customers.Count.Should().Be(1);
        customerByName.Customers.FirstOrDefault()!.Name.Should().Be(customerName);

        var customerByEmail = await customerService.Get(
            pageIndex: 1, 
            pageSize: 20, 
            Helpers.GetCancellationToken(), 
            customerEmail);

        customerByEmail.Customers.Count.Should().Be(1);
        customerByEmail.Customers.FirstOrDefault()!.Email.Should().Be(customerEmail);
    }

    private static async Task AddCustomers(int quantity, CanopusContext context)
    {
        var customers = new List<Customer>();
        
        for (var _ = 0; _ < quantity; _++)
        {
            customers.Add(new Customer());
        }

        await context.AddRangeAsync(customers);

        await context.SaveChangesAsync();
    }
}