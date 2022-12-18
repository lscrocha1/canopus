using System.Diagnostics.CodeAnalysis;
using Canopus.API.DTOs;
using Canopus.API.Responses;

namespace Canopus.API.Adapters;

[ExcludeFromCodeCoverage]
public static class CustomerAdapter
{
    public static MultipleResponse<CustomerDto> MapResponse(
        (IList<CustomerDto> Customers, 
        PaginationDto PaginationDto) result)
    {
        return new MultipleResponse<CustomerDto>(
            result.PaginationDto.TotalItems,
            result.PaginationDto.PageIndex,
            result.PaginationDto.PageSize,
            result.Customers);
    }
}