using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.DTOs;

[ExcludeFromCodeCoverage]
public class PaginationDto
{
    public PaginationDto(int totalItems, int pageIndex, int pageSize)
    {
        TotalItems = totalItems;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    public int TotalItems { get; }

    public int PageIndex { get; }

    public int PageSize { get; }
}