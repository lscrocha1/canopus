using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Responses;

[ExcludeFromCodeCoverage]
public class MultipleResponse<T> where T : class
{
    public MultipleResponse(int totalItems, int pageIndex, int pageSize, IList<T> items)
    {
        TotalItems = totalItems;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Items = items;
    }

    /// <summary>
    /// Total of items available for this set, example: there's 100 customers,
    /// the Items will contain only at maximum the PageSize quantity, but totalItems would be 100.
    /// </summary>
    public int TotalItems { get; }

    /// <summary>
    /// The index of the current page of items
    /// </summary>
    public int PageIndex { get; }

    /// <summary>
    /// The maximum value that can return, does not mean that will be equal to Items length.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// The items itself.
    /// </summary>
    public IList<T> Items { get; }
}