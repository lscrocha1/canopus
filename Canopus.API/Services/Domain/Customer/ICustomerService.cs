using Canopus.API.DTOs;

namespace Canopus.API.Services.Domain.Customer;

public interface ICustomerService
{
    Task<(IList<CustomerDto> Customers, PaginationDto PaginationDto)> Get(
        int pageIndex,
        int pageSize,
        CancellationToken token,
        string search = "");

    Task<CustomerDto> Add(
        string name, 
        string email,
        CancellationToken token);
}