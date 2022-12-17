using Canopus.API.DTOs;

namespace Canopus.API.Services.Domain.Customer;

public interface ICustomerService
{
    Task<(IList<CustomerDto> customers, PaginationDto dto)> Get(
        int pageIndex,
        int pageSize,
        string search = "");
}