using Canopus.API.DTOs;
using Canopus.API.Responses;

namespace Canopus.API.Services.Application.Customer;

public interface ICustomerService
{
    Task<MultipleResponse<CustomerDto>> Get(int pageIndex, int pageSize, string search = "");

    Task<CustomerDto> AddCustomer(AddCustomerDto dto);
}