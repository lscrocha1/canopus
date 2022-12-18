using Canopus.API.Adapters;
using Canopus.API.DTOs;
using Canopus.API.Responses;

namespace Canopus.API.Services.Application.Customer;

public class CustomerService : ICustomerService
{
    private readonly Domain.Customer.ICustomerService _customerService;

    public CustomerService(Domain.Customer.ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<MultipleResponse<CustomerDto>> Get(
        int pageIndex, 
        int pageSize, 
        CancellationToken token, 
        string? search = "")
    {
        var result = await _customerService.Get(pageIndex, pageSize, token, search);

        return CustomerAdapter.MapResponse(result);
    }

    public async Task<SingleResponse<CustomerDto>> AddCustomer(AddCustomerDto dto, CancellationToken token)
    {
        var result = await _customerService.Add(dto, token);

        return new SingleResponse<CustomerDto>(result);
    }
}